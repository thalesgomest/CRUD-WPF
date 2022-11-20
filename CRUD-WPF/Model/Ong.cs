using System;

namespace CRUD_WPF.Model
{
    public class Ong : BaseNotifyPropertyChanged, ICloneable
    {
        private int id;
        private string nome;
        private string endereco;
        private string telefone;
        private string email;

        public Ong()
        {
        }

        public Ong(int id, string nome, string endereco, string telefone, string email)
        {
            this.id = id;
            this.nome = nome;
            this.endereco = endereco;
            this.telefone = telefone;
            this.email = email;
        }

        public int Id { get => id; set { SetField(ref id, value); } }
        public string Nome { get => nome; set { SetField(ref nome, value); } }
        public string Endereco { get => endereco; set { SetField(ref endereco, value); } }
        public string Telefone { get => telefone; set { SetField(ref telefone, value); } }
        public string Email { get => email; set { SetField(ref email, value); } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
