using Ex1.Models;
using Ex1.Service;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ex1.Domain
{
    public class DB
    {
        private readonly string connectionString = Config.ConnectionString;

        public string[] GetNameAuthors()
        {
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT login FROM Users";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(reader.GetString(0));
                }
            }
            return names.ToArray();
        }
        public string[] GetNamePosts()
        {
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT name FROM Posts";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(reader.GetString(0));
                }
            }
            return names.ToArray();
        }
        public string[] GetNameTags()
        {
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT name FROM Tags";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(reader.GetString(0));
                }
            }
            return names.ToArray();
        }

        public void InsertPost(PostCreate postCreate)
        {
            string sqlExpression = "InsertPost";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@Author",
                    Value = postCreate.Author
                };
                command.Parameters.Add(nameParam); 
                nameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = postCreate.Name
                };
                command.Parameters.Add(nameParam);
                nameParam = new SqlParameter
                {
                    ParameterName = "@Text",
                    Value = postCreate.Text
                };
                command.Parameters.Add(nameParam);

                command.ExecuteNonQuery();
            }
        }

        public int GetMaxIdPosts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT MAX(id) FROM Posts";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            return -1;
        }
        public int GetMaxIdTags()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT MAX(id) FROM Tags";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            return -1;
        }
        public int GetMaxIdUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT MAX(id) FROM Users";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            return -1;
        }
        public string GetNamePostById(int id)
        {
            string name = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT name FROM Posts WHERE id = " + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    name = reader.GetString(0);
                }
            }
            return name;
        }
        public string GetNameTagById(int id)
        {
            string name = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT name FROM Tags WHERE id = " + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    name = reader.GetString(0);
                }
            }
            return name;
        }
        public string GetNameUserById(int id)
        {
            string name = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT name FROM Users WHERE id = " + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    name = reader.GetString(0);
                }
            }
            return name;
        }
        public void InsertTag(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Tags (name) VALUES ('" + name + "')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
        public void InsertUser(UserCreate userCreate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Users VALUES ('" + userCreate.Name + "', '" + userCreate.Login + "', " +
                    "'" + userCreate.Sex + "', '" + userCreate.Date + "')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        public string[] GetTagsPostById(string id)
        {
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT tagId FROM TagsPosts WHERE postId = " + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(GetNameTagById(reader.GetInt32(0)));
                }
            }
            return names.ToArray();
        }
        public string[] GetPostsTagById(string id)
        {
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT postId FROM TagsPosts WHERE tagId = " + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    names.Add(GetNamePostById(reader.GetInt32(0)));
                }
            }
            return names.ToArray();
        }
        public void AddTagToPost(string nameTag, string idPost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO TagsPosts VALUES ((SELECT id from Tags where name = '" + nameTag + "'), " + idPost + ")";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
        public void AddPostToTag(string namePost, string idTag)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO TagsPosts VALUES (" + idTag + ", (SELECT id from Posts where name = '" + namePost + "'))";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
        public void AddInf(AddInformation addInformation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Informations VALUES (" + addInformation.Id + ", '" + addInformation.Hobby + "', '" + addInformation.Text + "', '" + addInformation.Img + "')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
