using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1DV406_Labb2_2.Models
{
    public static class MyValidationExtension// Valideringsklass
    {

        public static bool Validate(this object instance, out ICollection<ValidationResult> validationResults)// Utökning av klassen objekt för direktvalidering
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }

}