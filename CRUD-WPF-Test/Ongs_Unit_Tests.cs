using Autofac.Extras.Moq;
using CRUD_WPF.Interfaces;
using CRUD_WPF.Model;
using CRUD_WPF.Model.Databse;
using Moq;

namespace CRUD_WPF_Test
{
    public class Ongs_Unit_Tests
    {
        private List<Ong> MockOngs()
        {
            List<Ong> output = new List<Ong>
            {
                new Ong
                {
                    Id = 1,
                    Nome = "Ong 1",
                   Endereco = "Endereço 1",
                   Telefone = "99999999999",
                    Email = "ong1@ong1",
                },
                new Ong
                {
                    Id = 2,
                    Nome = "Ong 2",
                    Endereco = "Endereço 2",
                    Telefone = "99999999999",
                    Email = "ong2@ong2",
                },
                new Ong
                {
                    Id = 3,
                    Nome = "Ong 3",
                    Endereco = "Endereço 3",
                    Telefone = "99999999999",
                    Email = "ong3@ong3",
                }
            };
            return output;
        }

        [Fact]
        public void BuscaOng()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDatabase>().Setup(x => x.BuscaOngs()).Returns(MockOngs());

                var dataBase = mock.Create<DataBaseGenerico>();
                var expected = MockOngs().ToList();
                var actual = dataBase.BuscaOngs().ToList();

                Assert.Equal(expected.Count, actual.Count);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].Id, actual[i].Id);
                    Assert.Equal(expected[i].Nome, actual[i].Nome);
                    Assert.Equal(expected[i].Endereco, actual[i].Endereco);
                    Assert.Equal(expected[i].Telefone, actual[i].Telefone);
                    Assert.Equal(expected[i].Email, actual[i].Email);
                }
            }
        }

        [Fact]
        public void CadastraOng()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var ong = new Ong
                {
                    Id = 4,
                    Nome = "Ong 4",
                    Endereco = "Endereço 4",
                    Telefone = "99999999999",
                    Email = "ong4@ong4",
                };

                var dataBase = mock.Create<DataBaseGenerico>();
                dataBase.CadastraOng(ong);

                mock.Mock<IDatabase>().Verify(x => x.CadastraOng(ong), Times.Once);
            }
        }

        [Fact]
        public void DeletaOng()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDatabase>().Setup(x => x.DeletaOng(It.IsAny<Ong>()));

                var dataBase = mock.Create<DataBaseGenerico>();
                var ong = MockOngs().First();

                dataBase.DeletaOng(ong);

                mock.Mock<IDatabase>().Verify(x => x.DeletaOng(It.IsAny<Ong>()), Times.Once);
            }
        }

        [Fact]
        public void EditaOng()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var ong = MockOngs()[0];
                mock.Mock<IDatabase>().Setup(x => x.EditaOng(ong));

                var dataBase = mock.Create<DataBaseGenerico>();

                dataBase.EditaOng(ong);

                mock.Mock<IDatabase>().Verify(x => x.EditaOng(ong), Times.Once);
            }
        }
    }
}