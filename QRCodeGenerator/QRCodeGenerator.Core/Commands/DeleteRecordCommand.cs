using QRCodeGenerator.Data.Interfaces;
using QRCodeGenerator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGenerator.Core.Commands
{
    public class DeleteRecordCommand : ICommand
    {
        private readonly IQRCodeStorageService _storageService;
        private readonly QRCodeRecord _record;

        public DeleteRecordCommand(IQRCodeStorageService storageService, QRCodeRecord record)
        {
            _storageService = storageService;
            _record = record;
        }

        public void Execute()
        {
            _storageService.DeleteRecord(_record.Content);
        }
    }
}