﻿using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;

            _context = new ApplicationContext(_options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarco()
        {
            var barco = new BarcoEntity { Nome = "Barco de Teste", Modelo = "Modelo 1", Ano = 2019, Tamanho = 40.5 };
            _barcoRepository.Adicionar(barco);

            Assert.Single(_context.Barco);
            Assert.Equal("Barco para teste", _context.Barco.First().Nome);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoCorreto()
        {
            var barco = new BarcoEntity { Nome = "Barco para testee", Modelo = "Modelo 1", Ano = 2021, Tamanho = 30.5 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            var result = _barcoRepository.ObterPorId(barco.Id);

            Assert.NotNull(result);
            Assert.Equal("Barco Testee", result.Nome);
        }

        [Fact]
        public void ObterTodos_DeveRetornarTodosOsBarcos()
        {
            _context.Barco.Add(new BarcoEntity { Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2020, Tamanho = 15.0 });
            _context.Barco.Add(new BarcoEntity { Nome = "Barco 2", Modelo = "Modelo 2", Ano = 2021, Tamanho = 40.0 });
            _context.SaveChanges();

            var result = _barcoRepository.ObterTodos();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Editar_DeveAtualizarBarco()
        {
            var barco = new BarcoEntity { Nome = "Barco de Teste", Modelo = "Modelo 22", Ano = 2022, Tamanho = 30.5 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            barco.Nome = "Barco Atualizado";
            _barcoRepository.Editar(barco);

            var result = _context.Barco.Find(barco.Id);
            Assert.Equal("Barco Atualizado", result.Nome);
        }

        [Fact]
        public void Remover_DeveDeletarBarco()
        {
            var barco = new BarcoEntity { Nome = "Barco para teste", Modelo = "Modelo z", Ano = 2021, Tamanho = 30.5 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            _barcoRepository.Remover(barco.Id);

            Assert.Empty(_context.Barco);
        }
    }
}
