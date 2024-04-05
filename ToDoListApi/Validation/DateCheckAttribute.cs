using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Validation
{
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Now)
                return new ValidationResult("Due date must require a future date to be selected.");
            return ValidationResult.Success;
        }
    }
}
