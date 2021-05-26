using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error

using MySql.Data.MySqlClient;
using MySql.Data.Types;

using KIT206_Ass2.Model;

namespace KIT206_Ass2.Database
{
    class Db_Researcher : DatabaseConn
    {
        //----------------------- Load All Researchers -----------------------
        // Return list of all researchers for ResearcherListView
        public static List<Researcher> LoadAllResearchers()
        {
            List<Researcher> researcher_list = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id, given_name, family_name, title, level, type FROM researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Note that in your assignment you will need to inspect the *type* of the
                    //researcher before deciding which kind of concrete class to create.
                    researcher_list.Add(new Researcher
                    {
                        ID = rdr.GetInt32(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        Title = ParseEnum<Title>(rdr.GetString(3)),
                        CurrentJobLevel = !DBNull.Value.Equals(rdr.GetValue(4)) ? ParseEnum<Level>(rdr.GetValue(4).ToString()) : ParseEnum<Level>("Student")
                        // If Level != Null -> Level = that value, else Level = "Student"
                    });
                    //Console.WriteLine($"Level = {rdr.GetValue(3)}, NULL = {DBNull.Value.Equals(level)}?");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return researcher_list;
        }

        /*
        Add--> LoadResearcher(int id)
        {
            if type = stuent -> LoadStudent
            if type = staff -> LoadStaff
        }
         */

        // ----------------------- Load Student -----------------------
        // Return details for student (id, name, title, unit, campus, email, photo, utasStart, currentStart, degree, supervisorName)
        public static Student LoadStudent(int id)
        {
            Student student = new Student();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                string sql = $"SELECT r1.id, r1.given_name, r1.family_name, r1.title, r1.unit, r1.campus, " +
                                    $"r1.email, r1.photo, r1.degree, r1.utas_start, r1.current_start, " +
                                    $"r2.given_name, r2.family_name" +
                             $" FROM researcher as r1, researcher as r2 " +
                             $"WHERE r1.id={id} and r1.supervisor_id=r2.id";
                //Console.WriteLine(sql); // For testing
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    student.ID = rdr.GetInt32(0);
                    student.GivenName = rdr.GetString(1);
                    student.FamilyName = rdr.GetString(2);
                    student.Title = ParseEnum<Title>(rdr.GetString(3));
                    student.Unit = rdr.GetString(4);
                    student.Campus = rdr.GetString(5);
                    student.Email = rdr.GetString(6);
                    student.Photo = rdr.GetString(7);
                    student.Degree = rdr.GetString(8);
                    student.UtasStart = rdr.GetDateTime(9);
                    student.CurrentStart = rdr.GetDateTime(10);
                    student.SupervisorName = rdr.GetString(11) + " " + rdr.GetString(12);
                    student.CurrentJobLevel = Level.Student;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading Student details");
                Console.WriteLine(e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return student;
        }

        // ----------------------- Load Staff -----------------------
        // Return details for staff (id, name, title, unit, campus, email, photo, utasStart, currentStart, level)
        public static Staff LoadStaff(int id)
        {
            Staff staff = new Staff();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                string sql = $"SELECT id, given_name, family_name, title, unit, campus, email, photo, utas_start, current_start, level " +
                             $"FROM researcher " +
                             $"WHERE id={id}";
                //Console.WriteLine(sql); // For testing
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    staff.ID = rdr.GetInt32(0);
                    staff.GivenName = rdr.GetString(1);
                    staff.FamilyName = rdr.GetString(2);
                    staff.Title = ParseEnum<Title>(rdr.GetString(3));
                    staff.Unit = rdr.GetString(4);
                    staff.Campus = rdr.GetString(5);
                    staff.Email = rdr.GetString(6);
                    staff.Photo = rdr.GetString(7);
                    staff.UtasStart = rdr.GetDateTime(8);
                    staff.CurrentStart = rdr.GetDateTime(9);
                    staff.CurrentJobLevel = ParseEnum<Level>(rdr.GetString(10));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading Staff details");
                Console.WriteLine(e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staff;
        }

        // ----------------------- Load previous positions (for staff) -----------------------
        // Return list of previous positions
        public static List<Position> LoadPreviousPositions(int id)
        {
            List<Position> prevPosList = new List<Position>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT level, start, end " +
                                                    $"FROM position WHERE id={id}", conn);

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    prevPosList.Add(new Position 
                    { 
                        JobLevel = ParseEnum<Level>(rdr.GetString(0)),
                        StartDate = rdr.GetDateTime(1),
                        EndDate = !DBNull.Value.Equals(rdr.GetValue(2)) ? rdr.GetDateTime(2) : default
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading previous positions for Staff");
                Console.WriteLine(e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return prevPosList;
        }


        // ----------------------- Load Supervisions (for Staff) -----------------------
        // Return list of student names that the staff supervises
        public static List<string> LoadSupervisions(int id)
        {
            List<string> studentNames = new List<string>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT given_name, family_name " +
                                                    $"FROM researcher WHERE supervisor_id={id}", conn);

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    studentNames.Add(rdr.GetString(0) + " " + rdr.GetString(1));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading Supervisions for Staff");
                Console.WriteLine(e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return studentNames;
        }


        // ----------------------- Load Publication List -----------------------
        // Return list of researcher's publications (title, year)
        public static List<Publication> LoadPublicationList(int id)
        {
            List<Publication> publicationList = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT title, year " +
                                                    "FROM publication as pub, researcher_publication as respub " +
                                                    $"WHERE pub.doi=respub.doi and researcher_id={id}", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    publicationList.Add(new Publication
                    {
                        Title = rdr.GetString(0),
                        Year = rdr.GetInt32(1)
                    });
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading researcher's publication list");
                Console.WriteLine(e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return publicationList;
        }
    }
}
