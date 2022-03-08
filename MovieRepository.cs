using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class MovieHallRepository
{
    private SqliteConnection connection;

    public MovieHallRepository(SqliteConnection connection)
    {
        this.connection = connection;

    }

    public long GetCount()
    {
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT COUNT(*) FROM customers";
        long count = (long)command.ExecuteScalar();
        return count;
    }

    public int GetTotalPages(long pageSize)
    {
        int totalPages = (int)Math.Ceiling(this.GetCount() / (double)pageSize);
        return totalPages == 0 ? 1 : totalPages;
    }


    public List<Customer> GetAll()
    {
        List<Customer> customers = new List<Customer>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM customers";
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            customers.Add(GetCustomer(reader));
        }
        reader.Close();
        return customers;
    }

    public List<Customer> GetPage(int pageNumber, long pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }
        List<Customer> Customers = new List<Customer>();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM customers LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
        command.Parameters.AddWithValue("$pageSize", pageSize);
        command.Parameters.AddWithValue("$pageNumber", pageNumber);
        SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Customer Customer = GetCustomer(reader);
            Customers.Add(Customer);
        }
        reader.Close();
        return Customers;
    }

    public bool Update(long id, Customer customer, bool passwordChanged)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"UPDATE customers SET name = $name, password = $password,
         WHERE Customer_id = $customer_id";
        command.Parameters.AddWithValue("$customer_id", id);
        if (passwordChanged)
        {
            customer.password = Authentication.GetHash(customer.password);
        }
        command.Parameters.AddWithValue("$username", customer.userName);
        command.Parameters.AddWithValue("$fullname", customer.fullname);
        command.Parameters.AddWithValue("$password", customer.password);
        command.Parameters.AddWithValue("$phoneNumber", customer.phoneNumber);
        command.Parameters.AddWithValue("$address", customer.address);

        int nChanged = command.ExecuteNonQuery();
        return nChanged == 1;

    }


    public Customer GetById(long id)
    {
        Customer customer = new Customer();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"SELECT * FROM customers WHERE customer_id = $customer_id";
        command.Parameters.AddWithValue("$customer_id", id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            customer = GetCustomer(reader);
        }
        else
        {
            return null;
        }
        reader.Close();
        return customer;
    }

    public int DeleteById(long id)
    {
        Customer customer = new Customer();
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText = @"DELETE FROM customers WHERE customer_id = $customer_id";
        command.Parameters.AddWithValue("$customer_id", id);
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
    public int Insert(Customer customer)
    {
        SqliteCommand command = this.connection.CreateCommand();
        command.CommandText =
        @"INSERT INTO customers (fullname, password, phoneNumber, address, username, userStatus, registrationTime) 
            VALUES ($fullname, $password, $phoneNumber, $address, $username, $userStatus, $registrationTime);
            
            SELECT last_insert_rowid();
            ";
        string hash = Authentication.GetHash(customer.password);
        command.Parameters.AddWithValue("$fullname", customer.fullname);
        command.Parameters.AddWithValue("$password", hash);
        command.Parameters.AddWithValue("$phoneNumber", customer.phoneNumber);
        command.Parameters.AddWithValue("$address", customer.address);
        command.Parameters.AddWithValue("$username", customer.userName);
        command.Parameters.AddWithValue("$userStatus", customer.userStatus);
        command.Parameters.AddWithValue("$registrationTime", customer.registrationTime.ToString("o"));
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

    public Customer GetCustomer(SqliteDataReader reader)
    {
        Customer customer = new Customer();
        customer.id = long.Parse(reader.GetString(0));
        customer.fullname = reader.GetString(1);
        customer.password = reader.GetString(2);
        customer.phoneNumber = reader.GetString(3);
        customer.address = reader.GetString(4);
        customer.userName = reader.GetString(5);
        customer.userStatus = int.Parse(reader.GetString(6)) == 1 ? true : false;
        customer.registrationTime = DateTime.Parse(reader.GetString(7));

        return customer;
    }
}