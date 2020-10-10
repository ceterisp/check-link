using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckLinkCLI2.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int? StatusCode { get; set; }
        public string LinkStatus { get; set; }
    }
}
