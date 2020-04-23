using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts
{
    public class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinContacts;Data Source=MAT");

        public void InsertContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"
                                INSERT INTO Contacts (FirstName, LastName, Phone, Address) 
                                VALUES (@FirstName,@LastName,@Phone,@Address)";
                //manera larga
                //SqlParameter firstName = new SqlParameter();
                //firstName.ParameterName = "@FirstName";
                //firstName.Value = contact.FirstName;
                //firstName.DbType = System.Data.DbType.String;
                //manero corta
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contact> GetContact(string search = null)
        {
            List<Contact> contact = new List<Contact>();
            try
            {
                conn.Open();
                string query = @"SELECT * FROM Contacts";

                SqlCommand command = new SqlCommand();

                if(!string.IsNullOrEmpty(search))
                {
                    string uno = "%" + search + "%";
                    query += @" WHERE FirstName LIKE @search OR LastName LIKE @search OR Phone LIKE @search OR Address LIKE @search";
                    command.Parameters.Add(new SqlParameter("@search",uno));
                }
                command.CommandText = query;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contact.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }

            }
                
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return contact;
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contacts
                                SET FirstName=@FirstName,
                                LastName=@LastName,
                                Phone=@Phone,
                                Address=@Address
                                WHERE Id=@Id";
                SqlParameter Id = new SqlParameter("@Id", contact.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(Id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts
                                    WHERE Id=@id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }
    }
}
