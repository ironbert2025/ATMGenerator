using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMGenerator.Domain.Entities
{
    public class AtmTemplate
    {
        public required string TemplateName { get; set; }   // e.g. ATM2060
        public int StopLoss { get; set; }
        public int Target { get; set; }
        public string? FilePath { get; set; }
    }
}
