using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    /// <summary>
    /// Example Entity used for illustration purposes.
    /// </summary>
    public class Entity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
