using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FoodDelivery.Models;
namespace FoodDelivery.Data
{
    public class FoodData
    {
        string _connectionString =

            "Data Source=DESKTOP-N5OCNK2\\SQLEXPRESS;Initial Catalog=FoodDelivery;Integrated Security=True;";

        public List<Food> GetFoods()
        {
            List<Food> foods = new List<Food>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Name, Price, IsAvailable FROM Foods";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Food food = new Food
                    {
                        Name = reader["Name"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };
                    foods.Add(food);
                }
                connection.Close();
            }

            return foods;
        }

        public int AddFood(Food food)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Foods (Name, Price, IsAvailable) VALUES (@Name, @Price, @IsAvailable)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", food.Name);
                command.Parameters.AddWithValue("@Price", food.Price);
                command.Parameters.AddWithValue("@IsAvailable", food.IsAvailable);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int UpdateFood(Food food)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Foods SET Price = @Price, IsAvailable = @IsAvailable WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", food.Name);
                command.Parameters.AddWithValue("@Price", food.Price);
                command.Parameters.AddWithValue("@IsAvailable", food.IsAvailable);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int DeleteFood(string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Foods WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", name);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }
    }
}

