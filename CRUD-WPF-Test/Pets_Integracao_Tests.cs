using CRUD_WPF.Interfaces;
using CRUD_WPF.Model;
using CRUD_WPF.Model.Databse;
using Moq;
using System.Collections.ObjectModel;

namespace CRUD_WPF_Test
{
    public class Pets_Integracao_Tests
    {
        private List<Pet> MockPets()
        {
            List<Pet> output = new List<Pet>
            {
                new Pet
                {
                    Id = 1,
                    Nome = "Pet 1",
                    Raca = "Raca 1",
                    Cor = "Cor 1",
                    Sexo = Sexo.Macho,
                    Porte = Porte.Pequeno,
                    Id_ong = 1,
                },
                new Pet
                {
                    Id = 2,
                    Nome = "Pet 2",
                    Raca = "Raca 2",
                    Cor = "Cor 2",
                    Sexo = Sexo.Fêmea,
                    Porte = Porte.Médio,
                    Id_ong = 2,
                },
                new Pet
                {
                    Id = 3,
                    Nome = "Pet 3",
                    Raca = "Raca 3",
                    Cor = "Cor 3",
                    Sexo = Sexo.Macho,
                    Porte = Porte.Grande,
                    Id_ong = 3,
                }
            };
            return output;
        }

        [Fact]
        public void BuscarPet()
        {
            Ong OngSelecionada = new Ong
            {
                Id = 1,
                Nome = "Ong 1",
                Endereco = "Endereço 1",
                Telefone = "99999999999",
                Email = "ong1@ong1",
            };
            Mock<IDatabase> mockDb = new Mock<IDatabase>();
            mockDb.Setup(x => x.BuscaPets(OngSelecionada)).Returns(MockPets());

            DataBaseGenerico conn = new DataBaseGenerico(mockDb.Object);

            ObservableCollection<Pet> ListaPets = new ObservableCollection<Pet>(conn.BuscaPets(OngSelecionada));

            var expected = MockPets();
            var actual = ListaPets;

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
                Assert.Equal(expected[i].Nome, actual[i].Nome);
                Assert.Equal(expected[i].Raca, actual[i].Raca);
                Assert.Equal(expected[i].Cor, actual[i].Cor);
                Assert.Equal(expected[i].Sexo, actual[i].Sexo);
                Assert.Equal(expected[i].Porte, actual[i].Porte);
                Assert.Equal(expected[i].Id_ong, actual[i].Id_ong);
            }
        }
    }
}
