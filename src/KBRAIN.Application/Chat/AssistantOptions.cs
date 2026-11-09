using System.ComponentModel.DataAnnotations;

namespace KBRAIN.Application.Chat
{
    public class AssistantOptions
    {
        public const string SectionName = "Assistant";
        [Required]
        [MinLength(1)]
        public List<string> SystemPrompt { get; set; } = [];
        [Range(0, 2)]
        public float Temperature { get; set; } = 0.1f;
    }
}
