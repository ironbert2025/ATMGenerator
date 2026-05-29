using ATMGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMGenerator.Application.Interfaces
{
    public interface IAtmTemplateRepository
    {
        /// <summary>Genera y guarda el archivo XML con los valores calculados.</summary>
        void Save(AtmTemplate template);

        /// <summary>Retorna todos los templates existentes en C:/Template ordenados por nombre.</summary>
        List<AtmTemplate> GetAll();
    }
}
