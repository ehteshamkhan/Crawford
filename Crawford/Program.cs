using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawford
{

    internal class Program
    {
        static string connectionString = "Server=interview-testing-server.database.windows.net; Database=Interview; User Id=TestLogin; Password=5D9ej2G64s3sd^;";
        static SqlConnection sqlconn = new SqlConnection(connectionString);
        static SqlConnection sqlconn2 = new SqlConnection(connectionString);
        static void Main(string[] args)
        {

            Console.WriteLine("Enter UserName: ");
            string userId = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            StringBuilder sb = new StringBuilder();
            using (sqlconn)
            {
                try
                {
                    sqlconn.Open();
                    sqlconn2.Open();
                    SqlCommand cmd = new SqlCommand("Select LossTypeId, LossTypeCode, LossTypeDescription from LossTypes", sqlconn);
                    SqlCommand cmdLogin = new SqlCommand("Select UserName, Password from Users", sqlconn2);
                    //var result = (int)cmd.ExecuteScalar();
                    //var result2 = cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //sb.Append(result2);

                    //if (result >0)
                    //{

                    //    Console.WriteLine("Successful Login");
                    //    Console.WriteLine(result+" total results");
                    //}

                    using (SqlDataReader readerLogin = cmdLogin.ExecuteReader())
                    {
                        if (readerLogin.HasRows)
                        {
                            while (readerLogin.Read())
                            {
                                if (readerLogin.GetString(0) == userId && readerLogin.GetString(1) == password)
                                {
                                    //readerLogin.Close();
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            Console.WriteLine("\t{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                                            Console.WriteLine("\t{0}\t\t{1}\t\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));


                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Didnt get any values from database...");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect Username or Password");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ReadKey();
                }
                sqlconn.Close();
                sqlconn2.Close();
            }
        }
    }
}
