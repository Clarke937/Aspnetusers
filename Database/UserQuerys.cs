using System;
using System.Collections.Generic;
using Holamundo.Models;
using MySqlConnector;
using System.Threading.Tasks;

namespace Holamundo.Database{

    public class UserQuerys{

        public DBConexion Db {get;}

        public UserQuerys(DBConexion db){
            Db = db;
            Db.Connection.Open();
        }

        public async Task<List<User>> selectAll(){
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"Select * from usuario";
            var reader = await cmd.ExecuteReaderAsync();
            var users = new List<User>();
            
            using(reader){
                while(await reader.ReadAsync()){
                    var user = new User(){
                        id_usuario = reader.GetInt32(0),
                        nombres = reader.GetString(1),
                        apellidos = reader.GetString(2),
                        correo = reader.GetString(3),
                        edad = reader.GetInt32(4)
                    };
                    users.Add(user);
                }
            }

            Db.Connection.Dispose();
            return users;
        }

        public async Task<int> Insertar(User user){
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"Insert into usuario values(@id,@nombres,@apellidos,@correo,@edad)";
            BindParams(cmd,user);
            await cmd.ExecuteNonQueryAsync();
            return (int) cmd.LastInsertedId;
        }

        public async Task<int> Actualizar(User user){
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"Update usuario set nombres = @nombres, apellidos = @apellidos, correo = @correo, edad = @edad where id_usuario = @id";
            BindParams(cmd,user);
            await cmd.ExecuteNonQueryAsync();
            return (int) cmd.UpdatedRowSource;
        }

        public async Task Delete(int id_usuario){
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM usuario WHERE id_usuario = " + id_usuario;
            await cmd.ExecuteNonQueryAsync();
        }


        private void BindParams(MySqlCommand cmd, User user){
            cmd.Parameters.Add(new MySqlParameter{
                ParameterName = "@id",
                DbType = System.Data.DbType.Int32,
                Value = user.id_usuario
            });
            cmd.Parameters.Add(new MySqlParameter{
                ParameterName = "@nombres",
                DbType = System.Data.DbType.String,
                Value = user.nombres
            });
            cmd.Parameters.Add(new MySqlParameter{
                ParameterName = "@apellidos",
                DbType = System.Data.DbType.String,
                Value = user.apellidos
            });
            cmd.Parameters.Add(new MySqlParameter{
                ParameterName = "@correo",
                DbType = System.Data.DbType.String,
                Value = user.correo
            });
            cmd.Parameters.Add(new MySqlParameter{
                ParameterName = "@edad",
                DbType = System.Data.DbType.Int32,
                Value = user.edad
            });
        }


    }

}