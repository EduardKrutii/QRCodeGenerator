using System;

namespace QRCodeGenerator.Data.Models
{
    public class QRCodeRecord
    {
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}