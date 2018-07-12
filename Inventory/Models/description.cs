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
            cmd.CommandText = @"INSERT INTO description (description) VALUES (@description);";

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
            cmd.CommandText = @"SELECT * FROM description;";
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
            cmd.CommandText = @"SELECT * FROM description WHERE id = (@searchId);";

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
        public List<Item> GetItems()
        {
            List<Item> allDescriptionItems = new List<Item> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items WHERE descriptionId = @description_id;";

            MySqlParameter descriptionId = new MySqlParameter();
            descriptionId.ParameterName = "@description_id";
            descriptionId.Value = this._id;
            cmd.Parameters.Add(descriptionId);


            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int itemId = rdr.GetInt32(0);
              string itemName = rdr.GetString(1);
              string itemPokemonType = rdr.GetString(2);
              int itemNumber = rdr.GetInt32(3);
              int ItemDescriptionId = rdr.GetInt32(4);

              Item newItem = new Item(itemName, itemPokemonType, itemNumber, ItemDescriptionId, itemId);
              allDescriptionItems.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allDescriptionItems;

        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM description;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
