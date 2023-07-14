using System.ComponentModel.DataAnnotations.Schema;

namespace MVCStartup.Models.DB.Entities
{
    // [Table("Request")]
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}