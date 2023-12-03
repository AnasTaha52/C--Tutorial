using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace Econtact.ContactBook
{
    internal class Contacts
    {
        //Getter Setter Properties

        public int contactID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string contactNo { get; set; }
        public string address { get; set; }
        public string gender { get; set; }

        static string connstring = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=1234";

        //Method for getting entire table 
        public DataTable Select()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            NpgsqlCommand comm = new NpgsqlCommand();
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();

                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select *from contacts";
                NpgsqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {

                    dataTable.Load(reader);
                    return dataTable;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                comm.Dispose();
                conn.Close();

            }
            return null;
        }

        //Insert Data into Database
        public bool InsertContact()
        {

            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            NpgsqlCommand comm = new NpgsqlCommand();

            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "INSERT INTO contacts (contact_id,first_name, last_name, contact_no, address, gender) VALUES (@contactId,@firstName,@lastName,@contactNo, @address, @gender)";
                comm.Parameters.AddWithValue("@contactId", this.contactID);
                comm.Parameters.AddWithValue("@firstName", this.firstName);
                comm.Parameters.AddWithValue("@lastName", this.lastName);
                comm.Parameters.AddWithValue("@contactNo", this.contactNo);
                comm.Parameters.AddWithValue("@address", this.address);
                comm.Parameters.AddWithValue("@gender", this.gender);
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                comm.Dispose();
                conn.Close();
            }
            return false;
        }
        //update method
        public bool updateContact()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            NpgsqlCommand comm = new NpgsqlCommand();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "UPDATE contacts SET first_name = @firstName,last_name = @lastName, contact_no = @contactNo, address = @address, gender =@gender WHERE contact_id = @contactID";
                comm.Parameters.AddWithValue("@contactId", this.contactID);
                comm.Parameters.AddWithValue("@firstName", this.firstName);
                comm.Parameters.AddWithValue("@lastName", this.lastName);
                comm.Parameters.AddWithValue("@contactNo", this.contactNo);
                comm.Parameters.AddWithValue("@address", this.address);
                comm.Parameters.AddWithValue("@gender", this.gender);
                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
            }
            finally
            {
                comm.Dispose();
                conn.Close();
            }
            return false;
        }

        //delete method 
        public bool deleteContact()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            NpgsqlCommand comm = new NpgsqlCommand();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "DELETE from contacts where contact_id=@contactId";
                comm.Parameters.AddWithValue("@contactId", this.contactID);

                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                comm.Dispose();
                conn.Close();
            }
            return false;
        }

        // searching 
        public DataTable search(string str)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            NpgsqlCommand comm = new NpgsqlCommand();
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;

                comm.CommandText = "SELECT * FROM contacts WHERE first_name LIKE @param OR last_name LIKE @param OR address LIKE @param OR contact_no LIKE @param";
                comm.Parameters.AddWithValue("@param", "%" + str + "%");

                NpgsqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {

                    dataTable.Load(reader);
                    return dataTable;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                comm.Dispose();
                conn.Close();
            }
            return null;
        }
    }
}
