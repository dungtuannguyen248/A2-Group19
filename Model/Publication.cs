using System;
using System.Collections.Generic;
using System.Text;

namespace KIT206_Ass2.Model
{
    public enum PublicationType { Conference, Journal, Other};
    class Publication
    {
        public string Doi { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int Year { get; set; }
        public PublicationType Type { get; set; }
        public string CiteAs { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public int Age
        {
            get
            {
                return (DateTime.Today - AvailabilityDate).Days;
            }
        }
    }
}
