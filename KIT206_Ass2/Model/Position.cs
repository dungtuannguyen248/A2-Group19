using System;
using System.Collections.Generic;
using System.Text;

namespace KIT206_Ass2.Model
{
    class Position
    {
        public Level JobLevel { get; set; }
        public string JobTitle
        {
            get
            {
                if (JobLevel == Level.A)
                {
                    return "Postdoc";
                }
                else if (JobLevel == Level.B)
                {
                    return "Lecturer";
                }
                else if (JobLevel == Level.C)
                {
                    return "Senior Lecturer";
                }
                else if (JobLevel == Level.D)
                {
                    return "Associate Professor";
                }
                else if (JobLevel == Level.E)
                {
                    return "Professor";
                }
                else if (JobLevel == Level.Student)
                {
                    return "Student";
                }
                else
                {
                    return "";
                }
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
