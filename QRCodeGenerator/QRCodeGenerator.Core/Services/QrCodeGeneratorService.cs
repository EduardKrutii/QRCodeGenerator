using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using QRCodeGenerator.Core.Interfaces;
using QRCoder;
using QRCodeGenerator.Data.Models;

namespace QRCodeGenerator.Core.Services
{
    public class QrCodeGeneratorService : IQRCodeGeneratorService
    {
        public Bitmap Generate(string text, Color qrColor, Color backgroundColor, string logoPath = null)
        {
            var qrGenerator = new QRCoder.QRCodeGenerator();
            var data = qrGenerator.CreateQrCode(text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(data);
            Bitmap qrBitmap = qrCode.GetGraphic(20, qrColor, backgroundColor, drawQuietZones: true);

            if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
            {
                using var logo = new Bitmap(logoPath);
                int logoSize = qrBitmap.Width / 5;

                Bitmap resizedLogo = new Bitmap(logo, new Size(logoSize, logoSize));
                Bitmap finalLogo = new Bitmap(logoSize, logoSize);
                using (Graphics g = Graphics.FromImage(finalLogo))
                {
                    g.Clear(Color.White);
                    g.DrawImage(resizedLogo, 0, 0, logoSize, logoSize);
                }

                using var graphics = Graphics.FromImage(qrBitmap);
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(finalLogo, (qrBitmap.Width - logoSize) / 2, (qrBitmap.Height - logoSize) / 2);
            }

            return qrBitmap;
        }
    }
}