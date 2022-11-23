using CRUD_WPF.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CRUD_WPF.Model.Databse
{
    internal class PostgreSQLDb : IDatabase
    {
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private NpgsqlDataReader reader;
        private static string _host = "localhost";
        private static string _port = "5432";
        private static string _user = "postgres";
        private static string _password = "postgres";
        private static string _database = "wpf_crud";
        private string _query;

        public PostgreSQLDb()
        {
            conn = new NpgsqlConnection($"Server={_host};Username={_user};Database={_database};Port={_port};Password={_password};");
            cmd = new NpgsqlCommand();
            cmd.Connection = conn;
        }

        public IEnumerable<Ong> BuscaOngs()
        {
            _query = "SELECT * FROM ongs";
            cmd.CommandText = _query;
            Conectar();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Ong
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Endereco = reader.GetString(2),
                    Telefone = reader.GetString(3),
                    Email = reader.GetString(4)
                };
            }
            Desconectar();
        }
        public IEnumerable<Pet> BuscaPets(Ong ongSelecionada)
        {
            _query = $"SELECT * FROM pets WHERE id_ong = {ongSelecionada.Id}";
            cmd.CommandText = _query;
            Conectar();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Pet
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Raca = reader.GetString(2),
                    Cor = reader.GetString(3),
                };
            }
            Desconectar();
        }

        public void CadastraOng(Ong ong)
        {
            // conn.Open();
            _query = $@"INSERT INTO ongs (nome, endereco, telefone, email) VALUES (
             '{ong.Nome}', 
            '{ong.Endereco}', 
            '{ong.Telefone}', 
            '{ong.Email}')";
            ExecutarQuery(_query);
        }

        public void CadastraPet(Pet pet)
        {
            _query = $@"INSERT INTO pets (nome, raca, cor, sexo, porte, id_ong) VALUES (
             '{pet.Nome}',
             '{pet.Raca}',
             '{pet.Cor}',
             '{pet.Sexo}',
             '{pet.Porte}',
             {pet.Id_ong}
             )";
            ExecutarQuery(_query);
        }

        public void DeletaOng(Ong ong)
        {
            _query = $"DELETE FROM ongs WHERE id = {ong.Id}";
            ExecutarQuery(_query);
        }

        public void DeletaPet(Pet pet)
        {
            _query = $"DELETE FROM pets WHERE id = {pet.Id}";
            ExecutarQuery(_query);
        }

        public void EditaOng(Ong ong)
        {
            _query = $@"UPDATE ongs SET
                nome = '{ong.Nome}',
                endereco = '{ong.Endereco}',
                telefone = '{ong.Telefone}',
                email = '{ong.Email}' WHERE id = {ong.Id}";
            ExecutarQuery(_query);
        }

        public void EditaPet(Pet pet)
        {
            _query = $@"UPDATE pets SET
                nome = '{pet.Nome}',
                raca = '{pet.Raca}',
                cor = '{pet.Cor}',
                sexo = '{pet.Sexo}',
                porte = '{pet.Porte}',
                id_ong = {pet.Id_ong}
            WHERE id = {pet.Id}";
        }

        public void ResetaTabelas()
        {
            _query = "DROP TABLE IF EXISTS pets";
            ExecutarQuery(_query);
            _query = "DROP TABLE IF EXISTS ongs";
            ExecutarQuery(_query);
            _query = "DROP TYPE SEXO";
            ExecutarQuery(_query);
            _query = "DROP TYPE PORTE";
            ExecutarQuery(_query);
            _query = "CREATE TYPE SEXO AS ENUM ('Macho', 'Fêmea')";
            ExecutarQuery(_query);
            _query = "CREATE TYPE PORTE AS ENUM ('Pequeno', 'Médio', 'Grande')";
            ExecutarQuery(_query);
            _query = @"CREATE TABLE ongs (
                id SERIAL PRIMARY KEY,
                nome VARCHAR(255) NOT NULL,
                endereco VARCHAR(255) NOT NULL,
                telefone VARCHAR(255) NOT NULL,
                email VARCHAR(255) NOT NULL
            )";
            ExecutarQuery(_query);
            _query = @"CREATE TABLE IF NOT EXISTS Pets (
                id serial PRIMARY KEY,
                nome VARCHAR(255) NOT NULL,
                raca VARCHAR(255) NOT NULL,
                cor VARCHAR(255) NOT NULL,
                sexo SEXO NOT NULL,
                porte PORTE NOT NULL,
                id_ong INT, 
	            FOREIGN KEY(id_ong) 
    	            REFERENCES Ongs (id)
                ON DELETE CASCADE
                ON UPDATE CASCADE
              )";
            ExecutarQuery(_query);
            _query = @"INSERT INTO ongs (nome, endereco, telefone, email) VALUES (
                    'Instituto Luiza Mel',
                    'Lorem ipsum dolor sit amet, consectetur adipisci elit',
                    '(99) 99999-9999',
                    'luizamel@org.com'
                  ),
                  (
                    'Instituto Amanda Breda',
                    'Lorem ipsum dolor sit amet, consectetur adipisci elit',
                    '(99) 99999-9999',
                    'amandabreda@org.com'
                  ),
                  (
                    'Instituto Thales Gomes',
                    'Lorem ipsum dolor sit amet, consectetur adipisci elit',
                    '(99) 99999-9999',
                    'amandabreda@org.com'
                  )";
            ExecutarQuery(_query);
        }

        private void Conectar()
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Desconectar()
        {
            conn.Close();
        }

        private void ExecutarQuery(string _query)
        {
            try
            {
                Conectar();
                cmd.CommandText = _query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
