using CRUD_WPF.Interfaces;
using System.Collections.Generic;

namespace CRUD_WPF.Model.Databse
{
    public class DataBaseGenerico
    {
        private IDatabase db;

        public DataBaseGenerico(IDatabase newDatabase)
        {
            db = newDatabase;
        }

        public IEnumerable<Ong> BuscaOngs()
        {
            return db.BuscaOngs();
        }

        public IEnumerable<Pet> BuscaPets(Ong ongSelecionada)
        {
            return db.BuscaPets(ongSelecionada);
        }

        public void CadastraOng(Ong ong)
        {
            db.CadastraOng(ong);
        }

        public void CadastraPet(Pet pet)
        {
            db.CadastraPet(pet);
        }

        public void DeletaOng(Ong ong)
        {
            db.DeletaOng(ong);
        }

        public void DeletaPet(Pet pet)
        {
            db.DeletaPet(pet);
        }

        public void EditaOng(Ong ong)
        {
            db.EditaOng(ong);
        }

        public void EditaPet(Pet pet)
        {
            db.EditaPet(pet);
        }

        public void ResetaTabelas()
        {
            db.ResetaTabelas();
        }
    }
}
