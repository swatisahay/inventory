using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inventory.Models
{
  public class Item
  {
    private string _name;
    private string _pokemonType;
    private int _number;
    private int _id;
    public Item(string Name, string PokemonType, int Number, int Id = 0)
    {
      _name = Name;
      _pokemonType = PokemonType;
      _number = Number;
      _id = Id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string Name)
    {
      _name = Name;
    }
    public string GetPokemonType()
    {
      return _pokemonType;
    }
    public void SetPokemonType(string PokemonType)
    {
      _pokemonType = PokemonType;
    }
    public int GetNumber()
    {
      return _number;
    }
    public void SetNumber(int Number)
    {
      _number = Number;
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Item> GetAll()
    {
      // return _instances;
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemName = rdr.GetString(1);
        string itemPokemonType = rdr.GetString(2);
        int itemNumber = rdr.GetInt32(3);
        Item newItem = new Item(itemName, itemPokemonType, itemNumber, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }
    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool nameEquality = (this.GetName() == newItem.GetName());
        bool pokemonTypeEquality = (this.GetPokemonType() == newItem.GetPokemonType());
        bool numberEquality = (this.GetNumber() == newItem.GetNumber());
        return (idEquality && nameEquality && pokemonTypeEquality && numberEquality);
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (Name, PokemonType, Number) VALUES (@ItemName, @ItemPokemonType, @ItemNumber);";


      cmd.Parameters.Add(new MySqlParameter("@ItemName", _name));
      cmd.Parameters.Add(new MySqlParameter("@ItemPokemonType", _pokemonType));
      cmd.Parameters.Add(new MySqlParameter("@ItemNumber", _number));


      //
      // MySqlParameter PokemonType = new MySqlParameter();
      // PokemonType.ParameterName = "@ItemPokemonType";
      // PokemonType.Value = this._pokemonType;
      // cmd.Parameters.Add(PokemonType);
      //
      // MySqlParameter Number = new MySqlParameter();
      // Number.ParameterName = "@ItemNumber";
      // Number.Value = this._number;
      // cmd.Parameters.Add(Number);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;    // This line is new!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM items WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int itemId = 0;
        string itemName = "";
        string itemPokemonType = "";
        int itemNumber = 0;

        while (rdr.Read())
        {
          itemId = rdr.GetInt32(0);
          itemName = rdr.GetString(1);
          itemPokemonType = rdr.GetString(2);
          itemNumber = rdr.GetInt32(3);
        }
        Item foundItem = new Item(itemName, itemPokemonType, itemNumber, itemId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
   }
   public void Edit(string newName, string newPokemonType, int newNumber)
   {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"UPDATE items SET name =@newName, pokemontype= @newPokemonType, number=@newNumber WHERE id = @searchId;";

       MySqlParameter searchId = new MySqlParameter();
       searchId.ParameterName = "@searchId";
       searchId.Value = _id;
       cmd.Parameters.Add(searchId);

       MySqlParameter name = new MySqlParameter();
       name.ParameterName = "@newName";
       name.Value = newName;
       cmd.Parameters.Add(name);

       MySqlParameter pokemontype = new MySqlParameter();
       pokemontype.ParameterName = "@newPokemonType";
       pokemontype.Value = newPokemonType;
       cmd.Parameters.Add(pokemontype);

       MySqlParameter number = new MySqlParameter();
       number.ParameterName = "@newNumber";
       number.Value = newNumber;
       cmd.Parameters.Add(number);

       cmd.ExecuteNonQuery();
       _name = newName;
       _pokemonType = newPokemonType;
       _number = newNumber;

       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }
   }
   public void Delete()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@thisId";
     searchId.Value = _id;
     cmd.Parameters.Add(searchId);



     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

  }
}
