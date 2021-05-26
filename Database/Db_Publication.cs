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
    class Db_Publication : DatabaseConn
    {
        public static Publication LoadPublication(string title)
        {
            Publication pub = new Publication();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT doi, title, authors, year, type, cite_as, available FROM publication " +
                                                    $"WHERE title='{title}'", conn);
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    pub.Doi = rdr.GetString(0);
                    pub.Title = rdr.GetString(1);
                    pub.Authors = rdr.GetString(2);
                    pub.Year = rdr.GetInt32(3);
                    pub.Type = ParseEnum<PublicationType>(rdr.GetString(4));
                    pub.CiteAs = rdr.GetString(5);
                    pub.AvailabilityDate = rdr.GetDateTime(6);
                }

            }
            catch (MySqlException e)
            {
                // ReportError("loading publication", e);
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

            return pub;
        }
    }
}
