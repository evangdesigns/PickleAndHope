﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleAndHope.Models;
using Microsoft.Data.SqlClient;

namespace PickleAndHope.DataAccess
{
    public class PickleRepository
    { 

        static List<Pickle> _pickles = new List<Pickle>
        {
            new Pickle
            {
                Type = "Bread and Butter",
                NumberInStock = 5,
                Id = 1
            }
        };

        //Connection String (connectionstring.com)
        const string ConnectionString = "Server=localhost;Database=PickleAndHope;Trusted_Connection=True;";

        public void Add(Pickle pickle)
        {
            pickle.Id = _pickles.Max(x => x.Id) + 1;
            _pickles.Add(pickle);
        }

        public void Remove(string type)
        {
            throw new NotImplementedException();
        }

        public Pickle Update(Pickle pickle)
        {
            var pickleToupdate = GetByType(pickle.Type);

            pickleToupdate.NumberInStock += pickle.NumberInStock;

            return pickleToupdate;
        }

        public Pickle GetByType(string typeOfPickle)
        {
            //return _pickles.FirstOrDefault(p => p.Type == type);

            //Sql Connection
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var query = $@"select *
                        from Pickle
                        where Type = '@type'";

                //Sql Command
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("Type", typeOfPickle);

                //execute the command
                var reader = cmd.ExecuteReader();

                //map it
                if (reader.Read())
                {
                    var pickle = new Pickle
                    {
                        Id = (int)reader["Id"],
                        Type = (string)reader["Type"],
                        Price = (decimal)reader["Price"],
                        NumberInStock = (int)reader["NumberInStock"],
                        Size = (string)reader["Size"],
                    };

                    return pickle;
                }

                return null;
            }
           
        }

        public List<Pickle> GetAll()
        {
            //Sql Connection
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            //Sql Command
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select * from pickle";

            //sql data reader
            var reader = cmd.ExecuteReader();

            var pickles = new List<Pickle>();

            //Map results to C# things
            while(reader.Read())
            {
                pickles.Add(MapReaderToPickle(reader));

            }

            connection.Close();

            return pickles;
        }

        public Pickle GetById(int id)
        {
            //return _pickles.FirstOrDefault(pickle => pickle.Id == id);

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                var query = @"select *
                                    from Pickle
                                    where id = @id";

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapReaderToPickle(reader);
                }
                return null;
            }
        }

        Pickle MapReaderToPickle(SqlDataReader reader)
        {
            var pickle = new Pickle
            {
                Id = (int)reader["Id"],
                Type = (string)reader["Type"],
                Price = (decimal)reader["Price"],
                NumberInStock = (int)reader["NumberInStock"],
                Size = (string)reader["Size"],
            }; ;
            return pickle;
        }
    }
}
