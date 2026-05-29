using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMGenerator.Domain.Entities
{
    public class AtmTemplate
    {
        public required string TemplateName { get; set; }   // Ej: ATM2060
        public int StopLoss { get; set; }   // valor1
        public int Target { get; set; }   // valor2
        public string? FilePath { get; set; }   // Ruta completa del archivo XML
    }
}
