using CRUD_WPF.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CRUD_WPF.ViewModel
{
    public class MainWindowVM
    {
        public ObservableCollection<Ong> ListaOngs { get; private set; }

        public ObservableCollection<Pet> ListaPets { get; private set; }
        public Ong OngSelecionada { get; set; }
        public Pet PetSelecionado { get; set; }


        public ICommand AddNovaOng { get; private set; }
        public ICommand AddNovoPet { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand EditNovaOng { get; private set; }
        public ICommand EditNovoPet { get; private set; }


        public MainWindowVM()
        {
            ListaOngs = new ObservableCollection<Ong>() {
                new Ong()
                {
                    Id = 1,
                    Nome = "Instituto Luisa Mel",
                    Email = "instituto@ong.com",
                    Telefone = "999999999",
                    Endereco = "Lorem ipsum dolor sit amet"
                },
                  new Ong()
                {
                    Id = 2,
                    Nome = "Instituto Luisa Mel",
                    Email = "instituto@ong.com",
                    Telefone = "999999999",
                    Endereco = "Lorem ipsum dolor sit amet"
                }
            };

            ListaPets = new ObservableCollection<Pet>() {
                    new Pet()
                    {
                        Id = 1,
                        Nome = "Rex",
                        Raca = "SRD",
                        Cor = "Preto",
                        Sexo = Sexo.Macho,
                        Porte = Porte.Médio,
                        Id_ong = 1
                    },
                    new Pet()
                    {
                        Id = 2,
                        Nome = "Romeu",
                        Raca = "Shitzu",
                        Cor = "Cinza",
                        Sexo = Sexo.Macho,
                        Porte = Porte.Pequeno,
                        Id_ong = 1
                    }
                };

            IniciaComandos();
        }

        private void IniciaComandos()
        {

            AddNovaOng = new RelayCommand((object param) =>
            {
                Ong novaOng = new Ong();
                int maxId = ListaOngs.Last().Id;
                novaOng.Id = maxId + 1;
                CadastroOng telaCadastroOng = new CadastroOng();
                telaCadastroOng.DataContext = novaOng;
                telaCadastroOng.ShowDialog();

                if (telaCadastroOng.DialogResult == true)
                {
                    ListaOngs.Add(novaOng);
                }
            });

            AddNovoPet = new RelayCommand((object param) =>
            {
                Pet novoPet = new Pet();
                int maxId = ListaPets.Last().Id;
                novoPet.Id = maxId + 1;
                CadastroPet telaCadastroPet = new CadastroPet();
                telaCadastroPet.DataContext = novoPet;
                telaCadastroPet.ShowDialog();

                if (telaCadastroPet.DialogResult == true)
                {
                    ListaPets.Add(novoPet);
                }
            });

            Remove = new RelayCommand((object param) =>
            {
                if (OngSelecionada != null && PetSelecionado == null)
                {
                    ListaOngs.Remove(OngSelecionada);
                }
                else if (PetSelecionado != null)
                {
                    ListaPets.Remove(PetSelecionado);
                }
            });

            EditNovaOng = new RelayCommand((object param) =>
            {
                Ong ongEditada = (Ong)OngSelecionada.Clone();
                CadastroOng telaCadastroOng = new CadastroOng();
                telaCadastroOng.DataContext = ongEditada;
                telaCadastroOng.ShowDialog();

                if (telaCadastroOng.DialogResult == true)
                {
                    OngSelecionada.Nome = ongEditada.Nome;
                    OngSelecionada.Email = ongEditada.Email;
                    OngSelecionada.Telefone = ongEditada.Telefone;
                    OngSelecionada.Endereco = ongEditada.Endereco;
                }
            });

            EditNovoPet = new RelayCommand((object param) =>
            {
                Pet petEditado = (Pet)PetSelecionado.Clone();
                CadastroPet telaCadastroPet = new CadastroPet();
                telaCadastroPet.DataContext = petEditado;
                telaCadastroPet.ShowDialog();

                if (telaCadastroPet.DialogResult == true)
                {
                    PetSelecionado.Nome = petEditado.Nome;
                    PetSelecionado.Raca = petEditado.Raca;
                    PetSelecionado.Cor = petEditado.Cor;
                    PetSelecionado.Sexo = petEditado.Sexo;
                    PetSelecionado.Porte = petEditado.Porte;
                    PetSelecionado.Id_ong = petEditado.Id_ong;
                }
            });
        }
    }
}
