using QRCodeGenerator.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QRCodeGenerator.UI.Views
{
    public partial class HistoryWindow : Window
    {
        private readonly QrCodeStorageService _storageService = new QrCodeStorageService();

        public HistoryWindow()
        {
            InitializeComponent();
            LoadHistory();
        }

        private void LoadHistory()
        {
            HistoryList.ItemsSource = _storageService.LoadAll();
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            var storageService = new QrCodeStorageService();
            storageService.ClearHistory();
            HistoryList.ItemsSource = storageService.GetAllRecords();
        }

        private void DeleteByContent_Click(object sender, RoutedEventArgs e)
        {
            var text = DeleteContentTextBox.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var storageService = new QrCodeStorageService();
                storageService.DeleteRecord(text);
                HistoryList.ItemsSource = storageService.GetAllRecords();
            }
        }
    }
}