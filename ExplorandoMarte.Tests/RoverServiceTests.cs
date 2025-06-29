using ExplorandoMarte.Domain.Exceptions;
using System.Linq;
using NUnit.Framework;
using ExplorandoMarte.App.Services;
using ExplorandoMarte.Domain.Enums;

namespace ExplorandoMarte.Tests
{
    [TestFixture]
    public class RoverServiceTests
    {
        private RoverService _service;

        [SetUp]
        public void Setup()
        {
            _service = new RoverService(5, 5);
        }

        [Test]
        public void AddRover_Deve_MoverEGirarCorretamente()
        {
            _service.AddRover(1, 2, Direction.N, "LMLMLMLMM");
            var rovers = _service.GetRovers();

            Assert.AreEqual(1, rovers.Count);

            var rover = rovers[0];
            Assert.AreEqual(1, rover.Position.X);
            Assert.AreEqual(3, rover.Position.Y);
            Assert.AreEqual(Direction.N, rover.Direction);
        }

        //[Test]
        //public void AddRover_Deve_LancarExcecao_Quando_SairDosLimites()
        //{
        //    var ex = Assert.Throws<OutOfBoundsException>(() =>
        //      _service.AddRover(5, 5, Direction.N, "M") // Movimento que vai para fora do planalto
        //    );

        //    Assert.That(ex.Message, Does.Contain("fora dos limites").IgnoreCase);
        //}

        [Test]
        public void AddRover_Deve_Lancar_Excecao_Quando_Sair_Dos_Limites()
        {
            var ex = Assert.Throws<OutOfBoundsException>(() =>
                _service.AddRover(5, 5, Direction.N, "M")
            );

            Assert.That(ex.Message, Does.Contain("fora dos limites").IgnoreCase);
        }


        //[Test]
        //public void AddRover_Deve_LancarExcecao_Quando_PosicaoEstiverOcupada()
        //{
        //    // Adiciona uma sonda na posição inicial (1, 2)
        //    _service.AddRover(1, 2, Direction.N, "");

        //    // Tenta adicionar outra sonda na mesma posição
        //    var ex = Assert.Throws<PositionConflictException>(() =>
        //        _service.AddRover(1, 2, Direction.E, "")
        //    );

        //    Assert.That(ex.Message, Does.Contain("posição ocupada").IgnoreCase);
        //}

        [Test]
        public void AddRover_Deve_Lancar_Excecao_Quando_Posicao_Ja_Estiver_Ocupada()
        {
            // Arrange
            _service.AddRover(1, 2, Direction.N, "");

            // Act + Assert
            var ex = Assert.Throws<PositionConflictException>(() =>
                _service.AddRover(1, 2, Direction.E, "")
            );

            Assert.That(ex.Message, Does.Contain("sonda na posição").IgnoreCase);
        }

        [Test]
        public void AddRover_Deve_ExecutarSequenciaDeComandosCorretamente()
        {
            // Arrange: sonda em (1,2) apontando para o Norte
            _service.AddRover(1, 2, Direction.N, "LMLMLMLMM");

            // Act
            var rover = _service.GetRovers().First();

            // Assert: posição final esperada (1,3) Norte
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, rover.Position.X, "X está incorreto.");
                Assert.AreEqual(3, rover.Position.Y, "Y está incorreto.");
                Assert.AreEqual(Direction.N, rover.Direction, "Direção está incorreta.");
            });
        }

    }
}
