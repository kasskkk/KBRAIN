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
        [Range(0, int.MaxValue)]
        public int ContextSize { get; set; } = 8192;
    }
}
