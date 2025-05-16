using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using QRCodeGenerator.Core.Interfaces;
using QRCodeGenerator.Core.Services;
using QRCodeGenerator.Data.Interfaces;
using QRCodeGenerator.Data.Models;
using QRCodeGenerator.Data.Services;
using System.Windows.Forms;
using DrawingColor = System.Drawing.Color;

namespace QRCodeGenerator.UI.Views
{
    public partial class MainView : Window
    {
        private readonly IQRCodeGeneratorService _qrService;
        private readonly IQRCodeStorageService _storageService;

        private DrawingColor _qrColor = DrawingColor.Black;
        private DrawingColor _backgroundColor = DrawingColor.White;
        private string _logoPath = null;
        private string _savePath = null;

        public MainView()
        {
            InitializeComponent();
            _qrService = new QrCodeGeneratorService();
            _storageService = new QrCodeStorageService();
        }

        private void GenerateQrCode_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            if (string.IsNullOrWhiteSpace(input)) return;

            var bitmap = _qrService.Generate(input, _qrColor, _backgroundColor, _logoPath);
            QrImage.Source = ConvertBitmapToImageSource(bitmap);
            SaveImage(bitmap, input, forHistory: true);
        }

        private BitmapImage ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using var memory = new MemoryStream();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memory;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;
        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryWindow();
            historyWindow.Show();
        }

        private void ChooseForegroundColor_Click(object sender, RoutedEventArgs e)
        {
            var color = ChooseColor();
            if (color.HasValue) _qrColor = color.Value;
        }

        private void ChooseBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            var color = ChooseColor();
            if (color.HasValue) _backgroundColor = color.Value;
        }

        private void SelectLogo_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == true)
            {
                _logoPath = dialog.FileName;
            }
        }
        private void RemoveLogo_Click(object sender, RoutedEventArgs e)
        {
            _logoPath = null;
            System.Windows.MessageBox.Show("Logo removed. The next QR code will be generated without a logo.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string SaveImage(Bitmap bitmap, string content, bool forHistory = false)
        {
            string directory;

            if (forHistory)
            {
                directory = Path.Combine(Path.GetTempPath(), "QRCodeGeneratorHistory");
            }
            else if (!string.IsNullOrWhiteSpace(_savePath))
            {
                directory = Path.GetDirectoryName(_savePath);
            }
            else
            {
                directory = "SavedQRCodes";
            }

            Directory.CreateDirectory(directory);

            string filename = _savePath != null && !forHistory
                ? Path.GetFileName(_savePath)
                : $"qr_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            string fullPath = Path.Combine(directory, filename);
            bitmap.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

            if (forHistory)
            {
                _storageService.AddRecord(new QRCodeRecord
                {
                    Content = content,
                    ImagePath = fullPath,
                    GeneratedAt = DateTime.Now
                });
            }

            return fullPath;
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "PNG Image|*.png";
            dialog.FileName = $"qr_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            if (dialog.ShowDialog() == true)
            {
                _savePath = dialog.FileName;

                string input = InputTextBox.Text;
                if (string.IsNullOrWhiteSpace(input)) return;

                var bitmap = _qrService.Generate(input, _qrColor, _backgroundColor, _logoPath);
                var savedPath = SaveImage(bitmap, input);

                QrImage.Source = ConvertBitmapToImageSource(bitmap);
            }
        }

        private DrawingColor? ChooseColor()
        {
            var dialog = new ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var c = dialog.Color;
                return DrawingColor.FromArgb(c.A, c.R, c.G, c.B);
            }
            return null;
        }
        private string _currentTheme = "Light";

        private void ToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            _currentTheme = _currentTheme == "Light" ? "Dark" : "Light";
            ThemeManager.ApplyTheme(System.Windows.Application.Current, _currentTheme);
        }
    }
}