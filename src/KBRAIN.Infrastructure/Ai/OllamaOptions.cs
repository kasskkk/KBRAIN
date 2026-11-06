using System.ComponentModel.DataAnnotations;

namespace KBRAIN.Infrastructure.Ai
{
    public class OllamaOptions
    {
        public const string SectionName = "Ollama";
        [Required]
        public string EndPoint { get; set; } = string.Empty;
        [Required]
        public string ModelName { get; set; } = string.Empty;
        [Range(0, 8192)]
        public int ContextSize { get; set; } = 8192;
        [Range(1, 60)]
        public int TimeoutMinutes { get; set; } = 5;
        public bool Think { get; set; } = false;
    }
}
