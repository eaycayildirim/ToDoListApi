using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ToDoListApi.Validation;

namespace ToDoListApi.Model
{
    public class ToDoListTask
    {
        [UniqueId]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

        [DateCheck]
        public DateTime DueDate { get; set; }

        [EnumDataType(typeof(TaskStatus))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskStatus Status { get; set; }
    }
}
