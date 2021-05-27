using System;
using System.Collections.Generic;
using System.Text;

namespace KIT206_Ass2.Model
{
    public enum Title { Dr, Mr, Ms, Mrs};
    public enum Level { A, B, C, D, E, Student };

    class Researcher
    {
        public int ID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Title Title { get; set; }
        public string Unit { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public Level CurrentJobLevel { get; set; }
        public string CurrentJobTitle
        {
            get
            {
                if(CurrentJobLevel == Level.A)
                {
                    return "Postdoc";
                }
                else if (CurrentJobLevel == Level.B)
                {
                    return "Lecturer";
                }
                else if (CurrentJobLevel == Level.C)
                {
                    return "Senior Lecturer";
                }
                else if (CurrentJobLevel == Level.D)
                {
                    return "Associate Professor";
                }
                else if (CurrentJobLevel == Level.E)
                {
                    return "Professor";
                }
                else if (CurrentJobLevel == Level.Student)
                {
                    return "Student";
                } 
                else
                {
                    return "";
                }
            }
        }
        public DateTime UtasStart { get; set; }
        public DateTime CurrentStart { get; set; }
        public double Tenure
        {
            get
            {
                return (double)(DateTime.Today - UtasStart).Days / (double)365;
            }
        }

        // Only retrieve year, title
        public List<Publication> Publications { get; set; }
    }
}
