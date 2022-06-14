using System;
using Npgsql; 
using System.Collections.Generic;
public class StateFeaturesRepository
{
    private NpgsqlConnection connection;

    public StateFeaturesRepository(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public StateFeatures GetStateFeatures(string type)
    {
        StateFeatures stateFeatures = new StateFeatures();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"Select FROM statefeatures WHERE createdAt = MAX(createdAt) AND customerTypeTitle = $customerTypeTitle";
        command.Parameters.AddWithValue("$customerTypeTitle", type);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            stateFeatures = GetStateFeatures(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return stateFeatures;
    }

    public bool Update(long id, StateFeatures stateFeatures)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE statefeatures SET customerTypeTitle = $customerTypeTitle, 
         discount = $discount, upperLimit = $upperLimit, createdAt = $createdaAt
         WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$customerTypeTitle", stateFeatures.CustomerTypeTitle);
        command.Parameters.AddWithValue("$disciunt", stateFeatures.Discount);
        command.Parameters.AddWithValue("$upperLimit", stateFeatures.UpperLimit);
        command.Parameters.AddWithValue("$createdAt", DateTime.Now);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }
        public int DeleteById(long id)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM statefeatures WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        int nChanged = command.ExecuteNonQuery();
        if (nChanged == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Insert(StateFeatures stateFeatures)
    {
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO ticketPurchases (ustomerTypeTitle, discount, upperLimit, createdAt) 
            VALUES ($customerTypeTitle, $discount, $upperLimit, $createdaAt);
            
            SELECT last_insert_rowid();
            ";
        command.Parameters.AddWithValue("$customerTypeTitle", stateFeatures.CustomerTypeTitle);
        command.Parameters.AddWithValue("$disciunt", stateFeatures.Discount);
        command.Parameters.AddWithValue("$upperLimit", stateFeatures.UpperLimit);
        command.Parameters.AddWithValue("$createdAt", DateTime.Now);
        long newId = (long)command.ExecuteScalar();
        if (newId == 0)
        {
            return 0;
        }
        else
        {
            return (int)newId; ;
        }

    }


    private StateFeatures GetStateFeatures(NpgsqlDataReader reader)
    {
        StateFeatures stateFeatures = new StateFeatures();
        stateFeatures.id = long.Parse(reader.GetString(0));
        stateFeatures.CustomerTypeTitle = reader.GetString(1);
        stateFeatures.Discount = Double.Parse(reader.GetString(2));
        stateFeatures.UpperLimit = Double.Parse(reader.GetString(3));
        stateFeatures.createdAt = DateTime.Parse(reader.GetString(4));
    
        return stateFeatures;
    }
}