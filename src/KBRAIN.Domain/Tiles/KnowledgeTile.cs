namespace KBRAIN.Domain.Tiles
{
    public class KnowledgeTile
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public bool IsPinned { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

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
                IsPinned = false,
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

        public void Unpin()
        {
            IsPinned = false;
            LastUpdatedAt = DateTime.UtcNow;
        }

        private static void Validate(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty or whitespace", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Content cannot be empty or whitespace", nameof(content));
            }
        }

    }
}
