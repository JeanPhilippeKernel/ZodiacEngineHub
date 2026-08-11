using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Panzerfaust.Models
{
    internal class Scene
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "Default";
        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; } = true;
    }

    // All user-facing asset directories live under $(workingSpace)/Assets/.
    // Paths use the $(workingSpace) token, expanded at engine runtime.
    internal class AssetDirectories
    {
        [JsonPropertyName("textureDir")]
        public string TextureDirectory { get; set; } = "$(workingSpace)/Assets/Textures";

        [JsonPropertyName("soundDir")]
        public string SoundDirectory { get; set; } = "$(workingSpace)/Assets/Sounds";

        [JsonPropertyName("meshDir")]
        public string MeshDirectory { get; set; } = "$(workingSpace)/Assets/Meshes";

        [JsonPropertyName("materialDir")]
        public string MaterialDirectory { get; set; } = "$(workingSpace)/Assets/Materials";

        [JsonPropertyName("spriteDir")]
        public string SpriteDirectory { get; set; } = "$(workingSpace)/Assets/Sprites";

        [JsonPropertyName("environmentMapDir")]
        public string EnvironmentMapDirectory { get; set; } = "$(workingSpace)/Assets/EnvironmentMaps";
    }

    internal class SkyConfig
    {
        // Supported values: "atmosphere", "hdri", "skySphere"
        // atmosphere = procedural sky (default, no asset required)
        // hdri       = equirectangular HDR image; environmentMap names the .zenvmap file
        // skySphere  = simple gradient sky
        [JsonPropertyName("mode")]
        public string Mode { get; set; } = "atmosphere";

        // Filename of the .zenvmap file inside assetDirs.environmentMapDir.
        // Omitted when mode is "atmosphere" or "skySphere".
        [JsonPropertyName("environmentMap")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EnvironmentMap { get; set; } = null;
    }

    internal class ProjectConfigJson
    {
        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0.0";

        [JsonPropertyName("workingSpace")]
        public string WorkingSpace { get; set; } = ".";

        // Cooked scene files (.zescene)
        [JsonPropertyName("sceneDir")]
        public string SceneDirectory { get; set; } = "$(workingSpace)/Scenes";

        // Cooked engine-format data (.zemesh, .zematerial, .zetextures)
        [JsonPropertyName("sceneDataDir")]
        public string SceneDataDirectory { get; set; } = "$(workingSpace)/SceneData";

        // All asset import directories
        [JsonPropertyName("assetDirs")]
        public AssetDirectories AssetDirectories { get; set; } = new();

        [JsonPropertyName("sky")]
        public SkyConfig Sky { get; set; } = new();

        [JsonPropertyName("sceneList")]
        public IList<Scene> Scenes { get; set; } = new List<Scene> { new Scene() };

        public string ToJson() => JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}
