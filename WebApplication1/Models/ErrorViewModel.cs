// Models/ErrorViewModel.cs

using System;

namespace CatalogoFilmesTempo.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}