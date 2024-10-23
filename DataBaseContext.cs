using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class DatabaseContext
    {
        private readonly string connectionString = "Data Source=DESKTOP-3IQ7VK1;Initial Catalog=Module6DataBase;Integrated Security=True";

        public List<Employee> GetEmployees()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Имя, Фамилия, Возраст, Должность, Зарплата FROM Table_1", connection);
                using (var reader = command.ExecuteReader())
                {
                    var employees = new List<Employee>();
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            FirstName = reader["Имя"].ToString(),
                            LastName = reader["Фамилия"].ToString(),
                            Age = (int)reader["Возраст"],
                            Position = reader["Должность"].ToString(),
                            Salary = (int)reader["Зарплата"]
                        });
                    }
                    return employees;
                }
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Table_1 SET Возраст = @Age, Должность = @Position, Зарплата = @Salary WHERE Имя = @FirstName AND Фамилия = @LastName", connection);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.ExecuteNonQuery();
            }
        }

        public void SaveEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Check if employee exists
                var checkCommand = new SqlCommand("SELECT COUNT(*) FROM Table_1 WHERE Имя = @FirstName AND Фамилия = @LastName", connection);
                checkCommand.Parameters.AddWithValue("@FirstName", employee.FirstName);
                checkCommand.Parameters.AddWithValue("@LastName", employee.LastName);
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    UpdateEmployee(employee);
                }
                else
                {
                    var insertCommand = new SqlCommand("INSERT INTO Table_1 (Имя, Фамилия, Возраст, Должность, Зарплата) VALUES (@FirstName, @LastName, @Age, @Position, @Salary)", connection);
                    insertCommand.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    insertCommand.Parameters.AddWithValue("@LastName", employee.LastName);
                    insertCommand.Parameters.AddWithValue("@Age", employee.Age);
                    insertCommand.Parameters.AddWithValue("@Position", employee.Position);
                    insertCommand.Parameters.AddWithValue("@Salary", employee.Salary);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
       public void DeleteEmployee(Employee employee)
    {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Table_1 WHERE Имя = @FirstName AND Фамилия = @LastName", connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.ExecuteNonQuery();
            }

       }
    }
}
