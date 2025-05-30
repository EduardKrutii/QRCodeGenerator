using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QRCodeGenerator.Data.Interfaces;
using QRCodeGenerator.Data.Models;

namespace QRCodeGenerator.Data.Services
{
    public class QrCodeStorageService : IQRCodeStorageService
    {
        private const string JsonFilePath = "qrcodes.json";

        public void Save(QRCodeRecord record)
        {
            var records = LoadAll();
            records.Add(record);
            var json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, json);
        }

        public List<QRCodeRecord> LoadAll()
        {
            if (!File.Exists(JsonFilePath))
                return new List<QRCodeRecord>();

            var json = File.ReadAllText(JsonFilePath);
            return JsonSerializer.Deserialize<List<QRCodeRecord>>(json) ?? new List<QRCodeRecord>();
        }

        public List<QRCodeRecord> GetAllRecords()
        {
            return LoadAll();
        }

        public void AddRecord(QRCodeRecord record)
        {
            Save(record);
        }
        public IEnumerable<QRCodeRecord> FindByContent(string text)
        {
            var allRecords = LoadAll();
            return allRecords.FindAll(r => r.Content != null && r.Content.Contains(text, StringComparison.OrdinalIgnoreCase));
        }
        public void ClearHistory()
        {
            if (File.Exists(JsonFilePath))
            {
                File.Delete(JsonFilePath);
            }
        }
        public void DeleteRecord(string content)
        {
            var records = LoadAll();
            var updatedRecords = records.FindAll(r => r.Content != content);

            var json = JsonSerializer.Serialize(updatedRecords, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, json);
        }
    }
}