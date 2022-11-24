using CRUD_WPF.Model;
using System.Collections.Generic;

namespace CRUD_WPF.Interfaces
{
    public interface IDatabase
    {
        List<Ong> BuscaOngs();
        List<Pet> BuscaPets(Ong ongSelecionada);
        void CadastraOng(Ong ong);
        void CadastraPet(Pet pet);
        void DeletaOng(Ong ong);
        void DeletaPet(Pet pet);
        void EditaOng(Ong ong);
        void EditaPet(Pet pet);
        void ResetaTabelas();
    }
}
