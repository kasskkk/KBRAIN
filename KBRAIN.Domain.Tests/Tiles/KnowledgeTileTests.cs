using KBRAIN.Domain.Tiles;

namespace KBRAIN.Domain.Tests.Tiles
{
    public class KnowledgeTileTests
    {
        [Fact]
        public void Create_WithValidData_SetsTitleAndContent()
        {
            var title = "Firma";
            var content = "Sprzedajemy meble biurowe.";

            var tile = KnowledgeTile.Create(title, content);

            Assert.Equal(title, tile.Title);
            Assert.Equal(content, tile.Content);
        }

        [Fact]
        public void Create_WithValidData_IsNotPinned()
        {
            var tile = CreateValidTile();

            Assert.False(tile.IsPinned);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t\n")]
        public void Create_WithNullOrWhitespaceTitle_ThrowsArgumentException(string? invalidTitle)
        {
            var content = "Sprzedajemy meble biurowe.";

            var exception = Assert.Throws<ArgumentException>(() => KnowledgeTile.Create(invalidTitle!, content));
            Assert.Equal("title", exception.ParamName);
        }

        [Fact]
        public void Create_WithEmptyContent_ThrowsArgumentException()
        {
            var title = "Firma";
            var content = "";

            var exception = Assert.Throws<ArgumentException>(() => KnowledgeTile.Create(title, content));
            Assert.Equal("content", exception.ParamName);
        }

        [Fact]
        public void Update_WithValidData_ChangesTitleAndContent()
        {
            var tile = CreateValidTile();

            var newTitle = "Farma";
            var newContent = "Sprzedajemy krowy.";

            tile.Update(newTitle, newContent);

            Assert.Equal(newTitle, tile.Title);
            Assert.Equal(newContent, tile.Content);
        }

        [Fact]
        public void Update_WithInvalidData_DoesNotChangeTile()
        {
            var tile = CreateValidTile();

            var originalTitle = tile.Title;
            var originalContent = tile.Content;

            Assert.Throws<ArgumentException>(() => tile.Update("", "Sprzedajemy krowy."));

            Assert.Equal(originalTitle, tile.Title);
            Assert.Equal(originalContent, tile.Content);
        }

        [Fact]
        public void Pin_SetsIsPinnedToTrue()
        {
            var tile = CreateValidTile();

            tile.Pin();

            Assert.True(tile.IsPinned);
        }

        [Fact]
        public void Unpin_SetsIsPinnedToFalse()
        {
            var tile = CreateValidTile();

            tile.Pin();

            tile.Unpin();

            Assert.False(tile.IsPinned);
        }

        private static KnowledgeTile CreateValidTile()
        {
            var title = "Firma";
            var content = "Sprzedajemy meble biurowe.";

            var tile = KnowledgeTile.Create(title, content);

            return tile;
        }
    }
}
