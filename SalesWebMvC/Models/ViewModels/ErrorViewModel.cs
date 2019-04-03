using System;
using SalesWebMvC.Models.ViewModels;

namespace SalesWebMvC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}