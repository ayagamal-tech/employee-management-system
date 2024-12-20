using System;
using System.Collections.Generic; //to use lists
using System.Linq; //to query data collections
using System.Text; //to manipilate strings
using System.Threading.Tasks;   //for synchronous programming
using System.Data;  //provide classes for working for databases
using System.Data.SqlClient; 

namespace EmployeeManagementSystem
{
    class SalaryData
    {
        public string EmployeeID { set; get; } // 0
        public string Name { set; get; } // 1
        public string Gender { set; get; } // 2
        public string Contact { set; get; } // 3
        public string Position { set; get; } // 4
        public int Salary { set; get; } // 5

        
        SqlConnection connect
    = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\maria\Documents\employee.mdf;Integrated Security = True; Connect Timeout = 30");

        public List<SalaryData> salaryEmployeeListData()
        {
            List<SalaryData> listdata = new List<SalaryData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employees WHERE status = 'Active' " +
                        "AND delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {   
                        SqlDataReader reader = cmd.ExecuteReader();
                        //to iterate over each row of retrieved data
                        while (reader.Read())
                        {
                            SalaryData sd = new SalaryData();
                            sd.EmployeeID = reader["employee_id"].ToString();
                            sd.Name = reader["full_name"].ToString();
                            sd.Gender = reader["gender"].ToString();
                            sd.Contact = reader["contact_number"].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.Salary = (int)reader["salary"];

                            listdata.Add(sd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }

    }
}
