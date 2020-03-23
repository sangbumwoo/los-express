using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosExpress.Models
{
    public class ErrorMessage : ModelStateDictionary
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
