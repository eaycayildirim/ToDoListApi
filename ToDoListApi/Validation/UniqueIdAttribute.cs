using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Validation
{
    /// <summary>
    /// UniqueId validation attribute to ensure the ID is valid
    /// </summary>
    public class UniqueIdAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var id = (int?)value;
            if (id <= 0)
                return new ValidationResult("Id must be greater than zero");
            return ValidationResult.Success;
        }
    }
}
