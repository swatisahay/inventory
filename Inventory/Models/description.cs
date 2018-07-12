using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Inventory.Models
{
    public class Description
    {
        private string _description;
        private int _id;
        public Description(string description, int id = 0)
        {
            _description = description;
            _id = id;
        }
        public override bool Equals(System.Object otherDescription)
        {
            if (!(otherDescription is Description))
            {
                return false;
            }
            else
            {
                Description newDescription = (Description) otherDescription;
                return this.GetId().Equals(newDescription.GetId());
            }
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string Getdescription()
        {
            return _description;
        }
        public int GetId()
        {
            return _id;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO descriptions (description) VALUES (@description);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@description";
            description.Value = this._description;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Description> GetAll()
        {
            List<Description> allDescription = new List<Description> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM descriptions;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int decriptionId = rdr.GetInt32(0);
              string descriptionDescription = rdr.GetString(1);
              Description newDescription = new Description(descriptionDescription, decriptionId);
              allDescription.Add(newDescription);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allDescription;
        }
        public static Description Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM descriptions WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int descriptionid = 0;
            string descriptiondescription = "";

            while(rdr.Read())
            {
              descriptionid = rdr.GetInt32(0);
              descriptiondescription = rdr.GetString(1);
            }
              Description newDescription = new Description(descriptiondescription, descriptionid);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newDescription;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM descriptions;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
