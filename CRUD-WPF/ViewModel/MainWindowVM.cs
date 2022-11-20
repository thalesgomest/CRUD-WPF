using CRUD_WPF.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CRUD_WPF.ViewModel
{
    public class MainWindowVM
    {
        public ObservableCollection<Ong> ListaOngs { get; private set; }

        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Edit { get; private set; }

        public Ong OngSelecionada { get; set; }
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
            IniciaComandos();
        }
        private void IniciaComandos()
        {
            Add = new RelayCommand((object param) =>
            {
                Ong novaOng = new Ong();
                int id = ListaOngs.Last().Id;
                novaOng.Id = id + 1;
                CadastroOng telaCadastroOng = new CadastroOng();
                telaCadastroOng.DataContext = novaOng;
                telaCadastroOng.ShowDialog();

                if (telaCadastroOng.DialogResult == true)
                {
                    ListaOngs.Add(novaOng);
                }

            });

            Remove = new RelayCommand((object param) =>
            {
                ListaOngs.Remove(OngSelecionada);
            });
        }
    }
}
