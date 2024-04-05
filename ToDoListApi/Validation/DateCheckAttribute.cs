using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Validation
{
    /// <summary>
    /// DateCheck validation attribute to ensure due date is in the future
    /// </summary>
    public class DateCheckAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Now)
                return new ValidationResult("Due date must require a future date to be selected.");
            return ValidationResult.Success;
        }
    }
}
