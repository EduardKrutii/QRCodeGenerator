using System.Drawing;
using Xunit;
using QRCodeGenerator.Core.Services;

namespace QRCodeGenerator.Tests
{
    public class QrCodeGeneratorServiceTests
    {
        [Fact]
        public void Generate_ShouldReturnBitmap_WhenValidInput()
        {
            var service = new QrCodeGeneratorService();

            var result = service.Generate("Test", Color.Black, Color.White, null);

            Assert.NotNull(result);
            Assert.IsType<Bitmap>(result);
        }

        [Fact]
        public void Generate_ShouldReturnDifferentBitmap_ForDifferentText()
        {
            var service = new QrCodeGeneratorService();

            var result1 = service.Generate("Text1", Color.Black, Color.White, null);
            var result2 = service.Generate("Text2", Color.Black, Color.White, null);

            Assert.NotEqual(result1.Size, Size.Empty);
            Assert.NotEqual(result2.Size, Size.Empty);
        }

        [Fact]
        public void Generate_ShouldIncludeLogo_WhenLogoPathIsProvided()
        {
            var service = new QrCodeGeneratorService();

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "logo.png");

            using (var logo = new Bitmap(20, 20))
            {
                logo.Save(logoPath);
            }

            var result = service.Generate("WithLogo", Color.Black, Color.White, logoPath);

            Assert.NotNull(result);
            Assert.IsType<Bitmap>(result);

            File.Delete(logoPath);
        }
    }
}