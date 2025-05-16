using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using QRCodeGenerator.Data.Models;

namespace QRCodeGenerator.Core.Interfaces
{
    public interface IQRCodeGeneratorService
    {
        Bitmap Generate(string text, Color qrColor, Color backgroundColor, string logoPath = null);
    }
}