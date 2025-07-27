using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSol.Domain.Model
{
    public sealed class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueTime { get; set; }
        public string Status { get; set; }
        public string AditionalData { get; set; }
    }
}
