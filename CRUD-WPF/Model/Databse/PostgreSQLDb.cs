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

        public List<Ong> BuscaOngs()
        {
            _query = "SELECT * FROM ongs";
            cmd.CommandText = _query;
            List<Ong> ongs = new List<Ong>();
            Ong NovaOng;
            Conectar();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                NovaOng = new Ong();
                NovaOng.Id = reader.GetInt32(0);
                NovaOng.Nome = reader.GetString(1);
                NovaOng.Endereco = reader.GetString(2);
                NovaOng.Telefone = reader.GetString(3);
                NovaOng.Email = reader.GetString(4);
                ongs.Add(NovaOng);
            }
            Desconectar();
            return ongs;
        }
        public List<Pet> BuscaPets(Ong ongSelecionada)
        {
            _query = $"SELECT * FROM pets WHERE id_ong = {ongSelecionada.Id}";
            cmd.CommandText = _query;
            List<Pet> pets = new List<Pet>();
            Pet NovoPet;
            Conectar();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                NovoPet = new Pet();
                NovoPet.Id = reader.GetInt32(0);
                NovoPet.Nome = reader.GetString(1);
                NovoPet.Raca = reader.GetString(2);
                NovoPet.Cor = reader.GetString(3);
                if (reader.GetString(4) == "Macho")
                    NovoPet.Sexo = Sexo.Macho;
                else NovoPet.Sexo = Sexo.Fêmea;
                if (reader.GetString(5) == "Pequeno")
                    NovoPet.Porte = Porte.Pequeno;
                else if (reader.GetString(5) == "Médio")
                    NovoPet.Porte = Porte.Médio;
                else
                    NovoPet.Porte = Porte.Grande;
                NovoPet.Id_ong = reader.GetInt32(6);
                pets.Add(NovoPet);
            }
            Desconectar();
            return pets;
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
