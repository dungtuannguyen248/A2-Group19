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
    abstract class DatabaseConn
    {   
        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        // Convert database string to enum type
        // This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }


        // Creates and returns (but does not open) the connection to the database.
        protected static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3};SslMode=none", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }


        


        //public static List<Position> LoadPositions()
        //{

        //}

        

        
        /*private static bool reportingErrors = false;
        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
    }
}

