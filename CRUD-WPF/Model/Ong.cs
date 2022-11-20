namespace CRUD_WPF.Model
{
    public class Ong
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

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Email { get => email; set => email = value; }
    }
}
