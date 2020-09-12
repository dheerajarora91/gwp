using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CountryGWP.Models
{
    public class CountryGwpRequest
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public IEnumerable<string> Lob { get; set; }
    }
}
