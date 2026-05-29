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
        /// Business logic:
        ///   stopLoss = Ceiling(inputValue / 1.25)
        ///   target   = stopLoss * 3
        ///   name     = ATM{stopLoss}{target}
        /// Returns (template, saved): saved=false means the file already existed and was not overwritten.
        /// </summary>
        public (AtmTemplate Template, bool Saved) Execute(double inputValue)
        {
            if (inputValue <= 0)
                throw new ArgumentException("Value must be greater than zero.");

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
    /// Returns all templates saved on disk.
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

