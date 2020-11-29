using System.ComponentModel.DataAnnotations;

namespace CheckLinkCLI2.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        public string LinkStatus { get; set; }
        public int? StatusCode { get; set; }
        public string Url { get; set; }
    }
}