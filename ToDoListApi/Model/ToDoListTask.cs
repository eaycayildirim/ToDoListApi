using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ToDoListApi.Validation;

namespace ToDoListApi.Model
{
    /// <summary>
    /// Represents the task in ToDo list
    /// </summary>
    public class ToDoListTask
    {
        /// <summary>
        /// Unique identifier of the task
        /// </summary>
        [UniqueId]
        public int Id { get; set; }

        /// <summary>
        /// The title of the task
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The description of the task
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The due date of the task
        /// </summary>
        [DateCheck]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// The status of the task
        /// </summary>
        [EnumDataType(typeof(TaskStatus))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskStatus Status { get; set; }
    }
}
