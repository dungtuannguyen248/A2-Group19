using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error

using MySql.Data.MySqlClient;
using MySql.Data.Types;
using KIT206_Ass2.Model;
using KIT206_Ass2.Database;

namespace KIT206_Ass2
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start ----- Loading -----");

            // 1. -------- Load All Researchers
            //displayResearchers();

            //2. -------- Load Student
            //int student_id = 123469;
            //Student student = Db_Researcher.LoadStudent(student_id);
            //student.Publications = Db_Researcher.LoadPublicationList(student_id);
            //Console.WriteLine(student.CurrentJobTitle);


            // 3. -------- Load Staff
            //int staff_id = 123462;
            //Staff staff = Db_Researcher.LoadStaff(staff_id);
            //staff.Publications = Db_Researcher.LoadPublicationList(staff_id);
            //staff.PrevPositions = Db_Researcher.LoadPreviousPositions(staff_id);
            //Console.WriteLine(staff.CurrentJobLevel);
            //Console.WriteLine(staff.CurrentJobTitle);

            //foreach (Position pos in staff.PrevPositions)
            //{
            //    if (pos.EndDate == DateTime.MinValue) // End date = NULL
            //    {
            //        Console.WriteLine($"{pos.JobTitle}: {pos.StartDate.ToString("dd/MM/yyyy")} --- Now");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{pos.JobTitle}: {pos.StartDate.ToString("dd/MM/yyyy")} --- {pos.EndDate.ToString("dd/MM/yyyy")}");
            //    }
            //}

            //Console.WriteLine("List of researcher's publications:");
            //foreach (Publication pub in staff.Publications)
            //{
            //    Console.WriteLine(pub.Year + ", " + pub.Title);
            //}
            //Console.WriteLine("#Publications = " + staff.Publications.Count);


            // 4. -------- Load Supervisions (for staff)
            //List<string> student_names = Db_Researcher.LoadSupervisions(123461);
            //foreach (string name in student_names)
            //{
            //    Console.WriteLine(name);
            //}


            // 5. -------- Load previous Positions (for staff)
            //List<Position> prev_pos_list = Db_Researcher.LoadPreviousPositions(123464);
            //foreach (Position pos in prev_pos_list)
            //{
            //    if(pos.EndDate == DateTime.MinValue) // End date = NULL
            //    {
            //        Console.WriteLine($"{pos.JobTitle}: {pos.StartDate.ToString("dd/MM/yyyy")} --- Now");
            //    } 
            //    else
            //    {
            //        Console.WriteLine($"{pos.JobTitle}: {pos.StartDate.ToString("dd/MM/yyyy")} --- {pos.EndDate.ToString("dd/MM/yyyy")}");
            //    }
            //}


            // 6. -------- Load researcher's Publication list
            //List<Publication> publication_list = Db_Researcher.LoadPublicationList(123461);
            //Console.WriteLine("List of publications:");
            //foreach(Publication pub in publication_list)
            //{
            //    Console.WriteLine(pub.Year + ", " + pub.Title);
            //}
            //Console.WriteLine("#Publications = " + publication_list.Count);

            // 7. -------- Load Publication
            //string title = "Higher order pheromone models in ant colony optimisation";
            //displayPublicationDetails(title);



            //Console.WriteLine(DateTime.Now.Year - 3);

        }

        // ------- Display Researchers (ResearcherListView)
        public static void displayResearchers()
        {
            List<Researcher> researcher_list = Db_Researcher.LoadAllResearchers();
            foreach (Researcher res in researcher_list)
            {
                Console.WriteLine($"Display = {res.FamilyName}, {res.GivenName} ({res.Title}) ({res.ID}) -- Level = {res.CurrentJobLevel}");
            }
        }

        // ------- Display researcher's Publication list (ResearcherListView)
        public static void displayPublicationDetails(string title)
        {
            Publication pub = Db_Publication.LoadPublication(title);
            Console.WriteLine($"Doi = {pub.Doi}");
            Console.WriteLine($"Title = {pub.Title}");
            Console.WriteLine($"Authors = {pub.Authors}");
            Console.WriteLine($"Year = {pub.Year}");
            Console.WriteLine($"Cite as = {pub.CiteAs}");
            Console.WriteLine($"Availability date = {pub.AvailabilityDate}");
            Console.WriteLine($"Age = {pub.Age}");
            Console.WriteLine("Pub Year = " + pub.AvailabilityDate.Year);
        }
    }

}
