using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class TicketPurchaseRepository
{
    private SqliteConnection connection;

    public TicketPurchaseRepository(SqliteConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM ticketpurchases";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<TicketPurchase> GetAll()
    {
        List<TicketPurchase> ticketPurchases = new List<TicketPurchase>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM ticketpurchases";
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ticketPurchases.Add(GetTicketPurchase(reader));
        }
        reader.Close();
        return ticketPurchases;
    }

    public List<TicketPurchase> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<TicketPurchase> ticketPurchases = new List<TicketPurchase>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM ticketpurchases LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            TicketPurchase ticketPurchase = GetTicketPurchase(reader);
            ticketPurchases.Add(ticketPurchase);
        }
        reader.Close();
        return ticketPurchases;
    }

    public bool Update(long id, bool isCanceled)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE ticketpurchases SET isCanceled = $isCanceled, 
             WHERE ticket_id = $ticket_id";
        command.Parameters.AddWithValue("$ticket_id", id);
        command.Parameters.AddWithValue("$isCanceled", isCanceled);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }


    public TicketPurchase GetById(long id)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM ticketpurchases WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            ticketPurchase = GetTicketPurchase(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return ticketPurchase;
    }

    public int DeleteById(long id)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM ticketpurchases WHERE hall_id = $hall_id";
        command.Parameters.AddWithValue("$hall_id", id);
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

    public int Insert(TicketPurchase ticketPurchase)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO ticketPurchases (ticket_id, createdAt, price, customer_id, payment_way, isCanceled) 
            VALUES ($ticket_id, $createdAt, $price, $customer_id, $payment_way, $isCanceled);
            
            SELECT last_insert_rowid();
            ";
        command.Parameters.AddWithValue("$ticket_id", ticketPurchase.ticket_id);
        command.Parameters.AddWithValue("$createdAt", ticketPurchase.createdAt);
        command.Parameters.AddWithValue("$price", ticketPurchase.price);
        command.Parameters.AddWithValue("$customer_id", ticketPurchase.customer_id);
        command.Parameters.AddWithValue("$payment_way", ticketPurchase.payment_way);
        command.Parameters.AddWithValue("$isCanceled", ticketPurchase.isCanceled);
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

    public TicketPurchase GetTicketPurchase(SqliteDataReader reader)
    {
        TicketPurchase ticketPurchase = new TicketPurchase();
        ticketPurchase.id = long.Parse(reader.GetString(0));
        ticketPurchase.ticket_id = long.Parse(reader.GetString(1));
        ticketPurchase.createdAt = DateTime.Parse(reader.GetString(2));
        ticketPurchase.price = double.Parse(reader.GetString(3));
        ticketPurchase.customer_id = long.Parse(reader.GetString(4));
        ticketPurchase.payment_way = reader.GetString(5);
        ticketPurchase.isCanceled = reader.GetString(6) == "1" ? true : false;

        return ticketPurchase;
    }
}