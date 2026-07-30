using Panzerfaust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Panzerfaust.Service
{
    internal class ProjectService : IProjectService
    {
        private readonly string _cachePath = Models.AppPaths.Projects;
        private readonly string _fileExtension = ".pzf";

        private void EnsureCacheDirectoryIsPresent()
        {
            if (!Directory.Exists(_cachePath))
            {
                Directory.CreateDirectory(_cachePath);
            }
        }

        public async Task<Project?> CreateAsync(string name, string path, DateTime creationTime, DateTime updateTime)
        {
            EnsureCacheDirectoryIsPresent();

            Project p = new() { Name = name, Fullpath = path, CreationDate = creationTime, UpdateDate = updateTime };

            using var fs = File.Open(Path.Combine(_cachePath, $"{p.GetHash()}{_fileExtension}"), FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fs, p).ConfigureAwait(false);
            return p;
        }

        public async Task<IEnumerable<Project>> LoadProjectsAsync()
        {
            EnsureCacheDirectoryIsPresent();
            
            var output = new List<Project>();

            foreach (var file in Directory.EnumerateFiles(_cachePath))
            {
                if (new DirectoryInfo(file).Extension != _fileExtension) continue;

                try
                {
                    using var fs = File.OpenRead(file);
                    var project = await JsonSerializer.DeserializeAsync<Project>(fs).ConfigureAwait(false);
                    if (project != null) output.Add(project);
                }
                catch (JsonException)
                {
                    // Skip corrupted cache files
                }
            }

            return output;
        }

        public async Task SaveAsync(Project p)
        {
            EnsureCacheDirectoryIsPresent();

            using var fs = File.Open(Path.Combine(_cachePath, $"{p.GetHash()}{_fileExtension}"), FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fs, p).ConfigureAwait(false);
        }

        public async Task RenameAsync(Project p, string newName)
        {
            EnsureCacheDirectoryIsPresent();

            // Update projectConfig.json inside the project folder
            var projectConfigPath = Path.Combine(p.Fullpath, "projectConfig.json");
            if (File.Exists(projectConfigPath))
            {
                var raw = await File.ReadAllTextAsync(projectConfigPath).ConfigureAwait(false);
                using var doc = JsonDocument.Parse(raw);
                var updated = UpdateProjectConfigName(doc, newName);
                await File.WriteAllTextAsync(projectConfigPath, updated).ConfigureAwait(false);
            }

            // Update the .pzf cache file (hash changes with name, so delete old)
            var oldFile = Path.Combine(_cachePath, $"{p.GetHash()}{_fileExtension}");
            p.Name = newName;
            var newFile = Path.Combine(_cachePath, $"{p.GetHash()}{_fileExtension}");

            using var fs = File.Open(newFile, FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fs, p).ConfigureAwait(false);

            if (File.Exists(oldFile) && oldFile != newFile)
                File.Delete(oldFile);
        }

        private static string UpdateProjectConfigName(JsonDocument doc, string newName)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
            writer.WriteStartObject();
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                if (prop.Name == "projectName")
                    writer.WriteString("projectName", newName);
                else
                    prop.WriteTo(writer);
            }
            writer.WriteEndObject();
            writer.Flush();
            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }

        public Task<(bool, ErrorMessage?)> DeleteAsync(Project p)
        {
            return Task.Run(() =>
            {
                (bool, ErrorMessage?) result = (false, null);

                try
                {
                    EnsureCacheDirectoryIsPresent();

                    var pzffilepath = Path.Combine(_cachePath, $"{p.GetHash()}{_fileExtension}");
                    File.Delete(pzffilepath);

                    Directory.Delete(p.Fullpath, true);

                    result = (true, null);
                }
                catch (Exception e)
                {
                    result = (false, new() { Message = e.Message, Exception = e });
                }

                return result;
            });
        }
    }
}
