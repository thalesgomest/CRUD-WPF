using Autofac.Extras.Moq;
using CRUD_WPF.Interfaces;
using CRUD_WPF.Model;
using CRUD_WPF.Model.Databse;
using Moq;

namespace CRUD_WPF_Test
{
    public class Pets_Unit_Tests
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
        public void BuscaOng()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Ong OngSelecionada = new Ong
                {
                    Id = 1,
                    Nome = "Ong 1",
                    Endereco = "Endereço 1",
                    Telefone = "99999999999",
                    Email = "ong1@ong1",
                };
                mock.Mock<IDatabase>().Setup(x => x.BuscaPets(OngSelecionada)).Returns(MockPets().Where(x => x.Id_ong == OngSelecionada.Id).ToList());

                var dataBase = mock.Create<DataBaseGenerico>();
                var expected = MockPets().Where(x => x.Id_ong == OngSelecionada.Id).ToList();
                var actual = dataBase.BuscaPets(OngSelecionada).ToList();

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

        [Fact]
        public void CadastraPet()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var pet = new Pet
                {
                    Id = 4,
                    Nome = "Pet 4",
                    Raca = "Raca 4",
                    Cor = "Cor 4",
                    Sexo = Sexo.Macho,
                    Porte = Porte.Pequeno,
                    Id_ong = 1,
                };



                var dataBase = mock.Create<DataBaseGenerico>();
                dataBase.CadastraPet(pet);

                mock.Mock<IDatabase>().Verify(x => x.CadastraPet(pet), Times.Once);
            }
        }

        [Fact]
        public void DeletaPet()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDatabase>().Setup(x => x.DeletaPet(It.IsAny<Pet>()));

                var dataBase = mock.Create<DataBaseGenerico>();
                var pet = MockPets().First();

                dataBase.DeletaPet(pet);

                mock.Mock<IDatabase>().Verify(x => x.DeletaPet(It.IsAny<Pet>()), Times.Once);
            }
        }

        [Fact]
        public void EditaPet()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var pet = MockPets()[0];
                mock.Mock<IDatabase>().Setup(x => x.EditaPet(pet));

                var dataBase = mock.Create<DataBaseGenerico>();

                dataBase.EditaPet(pet);

                mock.Mock<IDatabase>().Verify(x => x.EditaPet(pet), Times.Once);
            }
        }
    }
}