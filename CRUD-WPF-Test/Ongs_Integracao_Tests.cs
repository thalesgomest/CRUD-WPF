using CRUD_WPF.Interfaces;
using CRUD_WPF.Model;
using CRUD_WPF.Model.Databse;
using Moq;
using System.Collections.ObjectModel;

namespace CRUD_WPF_Test
{
    public class Ongs_Integracao_Tests
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
        public void BuscarOng()
        {
            Mock<IDatabase> mockDb = new Mock<IDatabase>();
            mockDb.Setup(x => x.BuscaOngs()).Returns(MockOngs());

            DataBaseGenerico conn = new DataBaseGenerico(mockDb.Object);

            ObservableCollection<Ong> ListaOngs = new ObservableCollection<Ong>(conn.BuscaOngs());

            var expected = MockOngs();
            var actual = ListaOngs;

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

        [Fact]
        public void CadastraOng()
        {
            {
                Mock<IDatabase> mockDb = new Mock<IDatabase>();
                mockDb.Setup(x => x.CadastraOng(It.IsAny<Ong>()));

                DataBaseGenerico conn = new DataBaseGenerico(mockDb.Object);

                Ong NovaOng = new Ong
                {
                    Id = 4,
                    Nome = "Ong 4",
                    Endereco = "Endereço 4",
                    Telefone = "99999999999",
                    Email = "ong4@ong4",
                };

                conn.CadastraOng(NovaOng);

                mockDb.Verify(x => x.CadastraOng(It.IsAny<Ong>()), Times.Once);
            }
        }
    }
}
