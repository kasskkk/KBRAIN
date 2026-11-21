using System;
using System.Collections.Generic;
using System.Text;

namespace KBRAIN.Domain.Tiles
{
    public class KnowledgeTile
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public bool IsPinned { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; private set; } = DateTime.UtcNow;

        private KnowledgeTile() { }


        public static KnowledgeTile Create(string title, string content)
        {
            Validate(title, content);

            var now = DateTime.UtcNow;

            var knowledgeTile = new KnowledgeTile()
            {
                Id = Guid.CreateVersion7(),
                Title = title,
                Content = content,
                IsPinned = true,
                CreatedAt = now,
                LastUpdatedAt = now,
            };

            return knowledgeTile;
        }

        public void Update(string title, string content)
        {
            Validate(title, content);

            Title = title;
            Content = content;
            LastUpdatedAt = DateTime.UtcNow;
        }

        public void Pin()
        {
            IsPinned = true;
            LastUpdatedAt = DateTime.UtcNow;
        }
        private static void Validate(string title, string content)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title cannot be empty or whitespace", nameof(title));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("Content cannot be empty or whitespace", nameof(content));
            }
        }

        public void UnPin()
        {
            IsPinned = false;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
