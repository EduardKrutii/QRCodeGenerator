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
        public HistoryWindow()
        {
            InitializeComponent();
            var storageService = new QrCodeStorageService();
            var records = storageService.LoadAll();
            HistoryList.ItemsSource = records;
        }
    }
}