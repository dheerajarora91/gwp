using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CountryGWP.DataAccess.Layer.Entities
{
    public class CountryGwp
    {
        [Key]
        public int CountryGwpId { get; set; }
        public string Country { get; set; }
        public string Lob { get; set; }
        public double Y2008 { get; set; }
        public double Y2009 { get; set; }
        public double Y2010 { get; set; }
        public double Y2011 { get; set; }
        public double Y2012 { get; set; }
        public double Y2013 { get; set; }
        public double Y2014 { get; set; }
        public double Y2015 { get; set; }
        [NotMapped]
        public double Average { get; set; }
    }
}
