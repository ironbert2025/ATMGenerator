using ATMGenerator.Application.Interfaces;
using ATMGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMGenerator.Application.UseCases
{
    public class GenerateAtmTemplateUseCase
    {
        private readonly IAtmTemplateRepository _repository;

        public GenerateAtmTemplateUseCase(IAtmTemplateRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Lógica de negocio:
        ///   valor1 = Ceiling(inputValue / 1.25)
        ///   valor2 = valor1 * 3
        ///   nombre = ATM{valor1}{valor2}
        /// </summary>
        /// <summary>
        /// Returns (template, saved): saved=false means the file already existed and was not overwritten.
        /// </summary>
        public (AtmTemplate Template, bool Saved) Execute(double inputValue)
        {
            if (inputValue <= 0)
                throw new ArgumentException("El valor debe ser mayor que cero.");

            int stopLoss = (int)Math.Ceiling(inputValue / 1.25);
            int target = stopLoss * 3;

            var template = new AtmTemplate
            {
                StopLoss = stopLoss,
                Target = target,
                TemplateName = $"ATM{stopLoss}{target}"
            };

            bool saved = _repository.Save(template);
            return (template, saved);
        }
    }

    /// <summary>
    /// Retorna todos los templates guardados en disco.
    /// </summary>
    public class GetAtmTemplatesUseCase
    {
        private readonly IAtmTemplateRepository _repository;

        public GetAtmTemplatesUseCase(IAtmTemplateRepository repository)
        {
            _repository = repository;
        }

        public List<AtmTemplate> Execute() => _repository.GetAll();
    }
}

