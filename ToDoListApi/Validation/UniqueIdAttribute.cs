using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Validation
{
    public class UniqueIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var id = (int?)value;
            if (id <= 0)
                return new ValidationResult("Id must be greater than zero");
            return ValidationResult.Success;
        }
    }
}
