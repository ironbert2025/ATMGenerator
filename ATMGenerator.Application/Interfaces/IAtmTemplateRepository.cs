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
        /// <summary>Generates and saves the XML file. Returns false if the file already exists.</summary>
        bool Save(AtmTemplate template);

        /// <summary>Returns all existing templates in C:/Template sorted by name.</summary>
        List<AtmTemplate> GetAll();
    }
}
