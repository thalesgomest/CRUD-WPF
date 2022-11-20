using System;

namespace CRUD_WPF.Model
{
    public class Pet : BaseNotifyPropertyChanged, ICloneable
    {
        private int id;
        private string nome;
        private string raca;
        private string cor;
        private Sexo sexo;
        private Porte porte;
        private int id_ong;

        public Pet()
        {
        }

        public Pet(int id, string nome, string raca, string cor, Sexo sexo, Porte porte, int id_ong)
        {
            this.id = id;
            this.nome = nome;
            this.raca = raca;
            this.cor = cor;
            this.sexo = sexo;
            this.porte = porte;
            this.id_ong = id_ong;
        }

        public int Id { get => id; set { SetField(ref id, value); } }
        public string Nome { get => nome; set { SetField(ref nome, value); } }
        public string Raca { get => raca; set { SetField(ref raca, value); } }
        public string Cor { get => cor; set { SetField(ref cor, value); } }
        public Sexo Sexo { get => sexo; set { SetField(ref sexo, value); } }
        public Porte Porte { get => porte; set { SetField(ref porte, value); } }
        public int Id_ong { get => id_ong; set { SetField(ref id_ong, value); } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
