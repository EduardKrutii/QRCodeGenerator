using QRCodeGenerator.Data.Models;
using System.Collections.Generic;

namespace QRCodeGenerator.Data.Interfaces
{
    public interface IQRCodeStorageService
    {
        void AddRecord(QRCodeRecord record);
        List<QRCodeRecord> GetAllRecords();
        IEnumerable<QRCodeRecord> FindByContent(string text);
        void ClearHistory();
        void DeleteRecord(string content);
    }
}