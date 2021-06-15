using FluentValidation;
using MidasoftTest.Common.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.Domain.Validations
{
    public class FamilyGroupValidator: AbstractValidator<FamilyGroupRequest>
    {
        public FamilyGroupValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage("Name es requerido");
            RuleFor(x => x.SurName).NotNull()
               .WithMessage("SurName es requerido");
            RuleFor(x => x.Identification).NotNull()
                .WithMessage("Identification es requerido");
            RuleFor(x => x.UserName).NotNull()
                .WithMessage("UserName es requerido");
            RuleFor(x => x.Age).NotNull()
                .WithMessage("Age es requerido");
          
        }
        
    }
}
