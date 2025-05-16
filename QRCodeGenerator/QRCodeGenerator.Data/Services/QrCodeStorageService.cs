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
    }
}