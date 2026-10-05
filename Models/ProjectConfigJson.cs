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

    internal class SkyDefaults
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; } = "atmosphere";

        [JsonPropertyName("environmentMap")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? EnvironmentMap { get; set; }

        [JsonPropertyName("environmentIntensity")]
        public float EnvironmentIntensity { get; set; } = 1.0f;

        [JsonPropertyName("environmentTint")]
        public float[] EnvironmentTint { get; set; } = new[] { 1.0f, 1.0f, 1.0f, 1.0f };

        [JsonPropertyName("environmentYawRadians")]
        public float EnvironmentYawRadians { get; set; } = 0.0f;
    }

    internal class RenderingSettings
    {
        [JsonPropertyName("environment_lighting_quality")]
        public string EnvironmentLightingQuality { get; set; } = "standard";

        [JsonPropertyName("environment_lighting_budget_mb")]
        public int EnvironmentLightingBudgetMb { get; set; } = 384;

        // Preserve other rendering policies when a configuration is round-tripped.
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalProperties { get; set; }
    }

    internal class ProjectConfigJson
    {
        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0.0";

        [JsonPropertyName("workingSpace")]
        public string WorkingSpace { get; set; } = ".";

        [JsonPropertyName("sceneDir")]
        public string SceneDirectory { get; set; } = "$(workingSpace)/Scenes";

        // All asset import directories
        [JsonPropertyName("assetDirs")]
        public AssetDirectories AssetDirectories { get; set; } = new();

        [JsonPropertyName("sceneList")]
        public IList<Scene> Scenes { get; set; } = new List<Scene> { new Scene() };

        [JsonPropertyName("skyDefaults")]
        public SkyDefaults SkyDefaults { get; set; } = new();

        [JsonPropertyName("rendering")]
        public RenderingSettings Rendering { get; set; } = new();

        public string ToJson() => JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}
