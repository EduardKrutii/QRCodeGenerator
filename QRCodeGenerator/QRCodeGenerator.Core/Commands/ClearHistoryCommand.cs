using QRCodeGenerator.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerator.Core.Commands
{
    public class ClearHistoryCommand : ICommand
    {
        private readonly IQRCodeStorageService _storageService;

        public ClearHistoryCommand(IQRCodeStorageService storageService)
        {
            _storageService = storageService;
        }

        public void Execute()
        {
            _storageService.ClearHistory();
        }
    }
}