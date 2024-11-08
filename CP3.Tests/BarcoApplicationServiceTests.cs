using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos()
        {
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2020, Tamanho = 225.0 },
                new BarcoEntity { Id = 2, Nome = "Barco 2", Modelo = "Modelo 2", Ano = 2021, Tamanho = 110.0 }
            });

            var result = _barcoService.ObterTodosBarcos();

         
        }

        [Fact]
        public void RemoverBarco_DeveChamarMetodoRemover()
        {
            _repositoryMock.Setup(r => r.Remover(1)).Returns(new BarcoEntity());

            _barcoService.RemoverBarco(1);

            _repositoryMock.Verify(r => r.Remover(1), Times.Once);
        }
    }
}
