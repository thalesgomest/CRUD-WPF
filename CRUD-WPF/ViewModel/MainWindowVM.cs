using CRUD_WPF.Model;
using CRUD_WPF.Model.Databse;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CRUD_WPF.ViewModel
{
    public class MainWindowVM : BaseNotifyPropertyChanged
    {
        private Ong _ongSelecionada;
        public ObservableCollection<Pet> ListaPets { get; set; }
        public ObservableCollection<Ong> ListaOngs { get; private set; }

        public Ong OngSelecionada
        {
            get => _ongSelecionada;
            set
            {
                _ongSelecionada = value;
                if (OngSelecionada != null)
                    ListaPets = new ObservableCollection<Pet>(_conn.BuscaPets(OngSelecionada));
                RaisePropertyChanged(nameof(ListaPets));
            }
        }
        public Pet PetSelecionado { get; set; }
        public ICommand AddNovaOng { get; private set; }
        public ICommand AddNovoPet { get; private set; }
        public ICommand Remover { get; private set; }
        public ICommand EditOng { get; private set; }
        public ICommand EditPet { get; private set; }

        private readonly DataBaseGenerico _conn;


        public MainWindowVM()
        {
            _conn = new DataBaseGenerico(new PostgreSQLDb());

            try
            {
                //_conn.ResetaTabelas();
                ListaOngs = new ObservableCollection<Ong>(_conn.BuscaOngs());
                ListaPets = new ObservableCollection<Pet>();
                RaisePropertyChanged(nameof(ListaOngs));
                RaisePropertyChanged(nameof(ListaPets));
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }
            IniciaComandos();

        }

        private void IniciaComandos()
        {
            AddNovaOng = new RelayCommand((object param) =>
            {
                Ong novaOng = new Ong();
                CadastroOng telaCadastroOng = new CadastroOng();
                telaCadastroOng.DataContext = novaOng;
                telaCadastroOng.ShowDialog();
                if (telaCadastroOng.DialogResult == true)
                {
                    novaOng.Telefone = AplicarMascaraTelefone(novaOng.Telefone);
                    _conn.CadastraOng(novaOng);
                    ListaOngs = new ObservableCollection<Ong>(_conn.BuscaOngs());
                    RaisePropertyChanged(nameof(ListaOngs));

                }
            });

            AddNovoPet = new RelayCommand((object param) =>
            {
                //desabilitar botao adicionar se a ong não estiver selecionada

                Pet novoPet = new Pet();
                novoPet.Id_ong = OngSelecionada.Id;
                CadastroPet telaCadastroPet = new CadastroPet();
                telaCadastroPet.DataContext = novoPet;
                telaCadastroPet.ShowDialog();
                if (telaCadastroPet.DialogResult == true)
                {
                    _conn.CadastraPet(novoPet);
                    ListaPets = new ObservableCollection<Pet>(_conn.BuscaPets(OngSelecionada));
                    RaisePropertyChanged(nameof(ListaPets));
                }
            }, (object param) =>
            {
                if (OngSelecionada != null)
                    return true;
                return false;
            });

            Remover = new RelayCommand((object param) =>
            {
                if (OngSelecionada != null && PetSelecionado == null)
                {
                    _conn.DeletaOng(OngSelecionada);
                    ListaOngs = new ObservableCollection<Ong>(_conn.BuscaOngs());
                    ListaPets = new ObservableCollection<Pet>(_conn.BuscaPets(OngSelecionada));
                    RaisePropertyChanged(nameof(ListaOngs));
                    RaisePropertyChanged(nameof(ListaPets));
                }
                else if (OngSelecionada != null && PetSelecionado != null)
                {
                    _conn.DeletaPet(PetSelecionado);
                    ListaPets = new ObservableCollection<Pet>(_conn.BuscaPets(OngSelecionada));
                    RaisePropertyChanged(nameof(ListaPets));
                }
            });

            EditOng = new RelayCommand((object param) =>
            {
                if (OngSelecionada != null)
                {
                    Ong ongEditada = (Ong)OngSelecionada.Clone();
                    CadastroOng telaCadastroOng = new CadastroOng();
                    telaCadastroOng.DataContext = OngSelecionada;
                    telaCadastroOng.ShowDialog();

                    if (telaCadastroOng.DialogResult == true)
                    {
                        _conn.EditaOng(OngSelecionada);
                        ListaOngs = new ObservableCollection<Ong>(_conn.BuscaOngs());
                        RaisePropertyChanged(nameof(ListaOngs));
                    }
                }
            });

            EditPet = new RelayCommand((object param) =>
            {
                if (PetSelecionado != null)
                {
                    Pet petEditado = (Pet)PetSelecionado.Clone();
                    CadastroPet telaCadastroPet = new CadastroPet();
                    telaCadastroPet.DataContext = petEditado;
                    telaCadastroPet.ShowDialog();

                    if (telaCadastroPet.DialogResult == true)
                    {
                        _conn.EditaPet(petEditado);
                        ListaPets = new ObservableCollection<Pet>(_conn.BuscaPets(OngSelecionada));
                        RaisePropertyChanged(nameof(ListaPets));
                    }
                }
            }, (object param) =>
            {
                if (PetSelecionado != null)
                    return true;
                return false;
            });
        }

        private string AplicarMascaraTelefone(string strNumero)
        {
            // por omissão tem 10 ou menos dígitos
            string strMascara = "{0:(00) 0000-0000}";
            // converter o texto em número
            long lngNumero = Convert.ToInt64(strNumero);

            if (strNumero.Length == 11)
                strMascara = "{0:(00) 00000-0000}";

            return string.Format(strMascara, lngNumero);
        }
    }
}
