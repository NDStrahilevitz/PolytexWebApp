using System;
using System.ComponentModel.DataAnnotations;

namespace PolytexWebApp.CustomAttributes
{
    public class LargerThanToday : ValidationAttribute
    {
        public LargerThanToday(){}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            var mydate = (DateTime)value;
            var currdate = DateTime.Today;
            if((mydate - currdate).Days < 1){
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(){
            return $"Date must be larger than today.";
        }
    }
}