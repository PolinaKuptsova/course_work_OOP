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
        command.CommandText = @"Select * FROM statefeatures MAX(created_at) 
            WHERE customer_type_title = @customer_type_title";
        command.Parameters.AddWithValue("@customer_type_title", type);
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
        command.CommandText = @"UPDATE statefeatures SET customer_type_title = @customer_type_title, 
         discount = @discount, upper_limit = @upper_limit, created_at = @createda_at
         WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@customer_type_title", stateFeatures.CustomerTypeTitle);
        command.Parameters.AddWithValue("@disciunt", stateFeatures.Discount);
        command.Parameters.AddWithValue("@upper_limit", stateFeatures.UpperLimit);
        command.Parameters.AddWithValue("@created_at", DateTime.Now);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }
        public int DeleteById(long id)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        NpgsqlCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM statefeatures WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
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
        @"INSERT INTO statefeatures (customer_type_title, discount, upper_limit, created_at) 
            VALUES (@customer_type_title, @discount, @upper_limit, @created_at)
            RETURNING id
            ";
        command.Parameters.AddWithValue("@customer_type_title", stateFeatures.CustomerTypeTitle);
        command.Parameters.AddWithValue("@disciunt", stateFeatures.Discount);
        command.Parameters.AddWithValue("@upper_limit", stateFeatures.UpperLimit);
        command.Parameters.AddWithValue("@created_at", DateTime.Now);
        int newId = (int)command.ExecuteScalar();
        return newId;

    }


    private StateFeatures GetStateFeatures(NpgsqlDataReader reader)
    {
        StateFeatures stateFeatures = new StateFeatures();
        stateFeatures.id = reader.GetInt32(0);
        stateFeatures.CustomerTypeTitle = reader.GetString(1);
        stateFeatures.Discount = reader.GetDouble(2);
        stateFeatures.UpperLimit = reader.GetInt32(3);
        stateFeatures.createdAt = reader.GetDateTime(4);
    
        return stateFeatures;
    }
}