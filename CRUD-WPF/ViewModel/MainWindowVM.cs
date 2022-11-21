﻿using CRUD_WPF.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CRUD_WPF.ViewModel
{
    public class MainWindowVM : BaseNotifyPropertyChanged
    {
        private Ong _ongSelecionada;
        private ObservableCollection<Pet> ListaPetBase;
        public ObservableCollection<Pet> ListaPets { get; set; }
        public ObservableCollection<Ong> ListaOngs { get; private set; }

        public Ong OngSelecionada
        {
            get => _ongSelecionada;
            set
            {
                _ongSelecionada = value;
                if (OngSelecionada != null)
                    ListaPets = new ObservableCollection<Pet>(ListaPetBase.Where(x => x.Id_ong == OngSelecionada.Id));
                RaisePropertyChanged(nameof(ListaPets));
            }
        }
        public Pet PetSelecionado { get; set; }
        public ICommand AddNovaOng { get; private set; }
        public ICommand AddNovoPet { get; private set; }
        public ICommand Remover { get; private set; }
        public ICommand EditOng { get; private set; }
        public ICommand EditPet { get; private set; }


        public MainWindowVM()
        {
            ListaOngs = new ObservableCollection<Ong>() {
                new Ong()
                {
                    Id = 1,
                    Nome = "Instituto Luisa Mel",
                    Email = "instituto@ong.com",
                    Telefone = AplicarMascaraTelefone("35999160995"),
                    Endereco = "Lorem ipsum dolor sit amet"
                },
                  new Ong()
                {
                    Id = 2,
                    Nome = "Instituto Amanda Breda",
                    Email = "instituto@ong.com",
                    Telefone = AplicarMascaraTelefone("77777777777"),
                    Endereco = "Lorem ipsum dolor sit amet"
                }
            };

            ListaPetBase = new ObservableCollection<Pet>() {
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
                         Id_ong = 2
                     }
                 };
            IniciaComandos();
        }

        private void IniciaComandos()
        {
            AddNovaOng = new RelayCommand((object param) =>
            {
                Ong novaOng = new Ong();
                int maxId = 0;
                if (ListaOngs.Count > 0)
                {
                    maxId = ListaOngs.Last().Id;
                }
                novaOng.Id = maxId + 1;
                CadastroOng telaCadastroOng = new CadastroOng();
                telaCadastroOng.DataContext = novaOng;
                telaCadastroOng.ShowDialog();

                if (telaCadastroOng.DialogResult == true)
                {
                    novaOng.Telefone = AplicarMascaraTelefone(novaOng.Telefone);
                    ListaOngs.Add(novaOng);
                }
            });

            AddNovoPet = new RelayCommand((object param) =>
            {
                Pet novoPet = new Pet();
                int maxId = 0;
                int idOngSelecionada = OngSelecionada.Id;
                if (ListaPetBase.Count > 0)
                {
                    maxId = ListaPetBase.Last().Id;
                }
                novoPet.Id = maxId + 1;
                novoPet.Id_ong = idOngSelecionada;
                CadastroPet telaCadastroPet = new CadastroPet();
                telaCadastroPet.DataContext = novoPet;
                telaCadastroPet.ShowDialog();

                if (telaCadastroPet.DialogResult == true)
                {
                    ListaPets.Add(novoPet);
                    ListaPetBase.Add(novoPet);
                }
            });

            Remover = new RelayCommand((object param) =>
            {
                if (OngSelecionada != null && PetSelecionado == null)
                {
                    ListaOngs.Remove(OngSelecionada);
                }
                else if (PetSelecionado != null)
                {
                    ListaPets.Remove(PetSelecionado);
                    ListaPetBase.Remove(PetSelecionado);
                }
            });

            EditOng = new RelayCommand((object param) =>
            {
                if (OngSelecionada != null)
                {
                    Ong ongEditada = (Ong)OngSelecionada.Clone();
                    CadastroOng telaCadastroOng = new CadastroOng();
                    telaCadastroOng.DataContext = ongEditada;
                    telaCadastroOng.ShowDialog();

                    if (telaCadastroOng.DialogResult == true)
                    {
                        OngSelecionada.Nome = ongEditada.Nome;
                        OngSelecionada.Email = ongEditada.Email;
                        OngSelecionada.Telefone = AplicarMascaraTelefone(ongEditada.Telefone);
                        OngSelecionada.Endereco = ongEditada.Endereco;
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
                        PetSelecionado.Nome = petEditado.Nome;
                        PetSelecionado.Raca = petEditado.Raca;
                        PetSelecionado.Cor = petEditado.Cor;
                        PetSelecionado.Sexo = petEditado.Sexo;
                        PetSelecionado.Porte = petEditado.Porte;
                        PetSelecionado.Id_ong = petEditado.Id_ong;
                    }
                }
            });
        }
        // gerar lista de pets apenas com os pets da ong selecionada

        public string AplicarMascaraTelefone(string strNumero)
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
