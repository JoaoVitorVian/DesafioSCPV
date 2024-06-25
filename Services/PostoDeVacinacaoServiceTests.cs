using Application.DTO.PostoDeVacinacao;
using Application.Services;
using AutoMapper;
using CadastroPostosVacinacao.Domain.Entities;
using Infra.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class PostoDeVacinacaoServiceTests
{
    private readonly Mock<IPostoDeVacinacaoRepository> _mockRepository;
    private readonly Mock<IVacinaRepository> _mockVacinaRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly PostoDeVacinacaoService _service;
    private readonly List<PostoDeVacinacao> _mockPostosDeVacinacao;
    private readonly List<PostoDeVacinacaoDTO> _mockPostosDeVacinacaoDTO;

    public PostoDeVacinacaoServiceTests()
    {
        _mockRepository = new Mock<IPostoDeVacinacaoRepository>();
        _mockVacinaRepository = new Mock<IVacinaRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new PostoDeVacinacaoService(_mockRepository.Object, _mockMapper.Object, _mockVacinaRepository.Object);

        // Dados pré-cadastrados
        _mockPostosDeVacinacao = new List<PostoDeVacinacao>
        {
        new PostoDeVacinacao { Id = 1, Nome = "Posto 1", Endereco = "Endereço 1" },
        new PostoDeVacinacao { Id = 2, Nome = "Posto 2", Endereco = "Endereço 2" }
        };

        _mockPostosDeVacinacaoDTO = new List<PostoDeVacinacaoDTO>
        {
        new PostoDeVacinacaoDTO { Id = 1, Nome = "Posto 1", Endereco = "Endereço 1" },
        new PostoDeVacinacaoDTO { Id = 2, Nome = "Posto 2", Endereco = "Endereço 2" }
        };

        // Configuração dos mocks
        _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(_mockPostosDeVacinacao);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PostoDeVacinacaoDTO>>(_mockPostosDeVacinacao)).Returns(_mockPostosDeVacinacaoDTO);
    }


    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPostosDeVacinacao()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(_mockPostosDeVacinacao);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PostoDeVacinacaoDTO>>(_mockPostosDeVacinacao)).Returns(_mockPostosDeVacinacaoDTO);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(_mockPostosDeVacinacaoDTO, result);
    }


    [Fact]
    public async Task GetByIdAsync_ShouldReturnPostoDeVacinacao_WhenIdExists()
    {
        // Arrange
        var postoId = 1;
        var posto = _mockPostosDeVacinacao.First(p => p.Id == postoId);
        var postoDTO = _mockPostosDeVacinacaoDTO.First(p => p.Id == postoId);
        _mockRepository.Setup(repo => repo.Get(postoId)).ReturnsAsync(posto);
        _mockMapper.Setup(mapper => mapper.Map<PostoDeVacinacaoDTO>(posto)).Returns(postoDTO);

        // Act
        var result = await _service.GetByIdAsync(postoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(postoDTO, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenIdDoesNotExist()
    {
        // Arrange
        var postoId = 99;
        _mockRepository.Setup(repo => repo.Get(postoId)).ReturnsAsync((PostoDeVacinacao)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(postoId));
        Assert.Equal("Posto não encontrado.", exception.Message);
    }

    [Fact]
    public async Task SearchByName_ShouldReturnMatchingPostos()
    {
        // Arrange
        var searchName = "Posto";
        var matchingPostos = _mockPostosDeVacinacao.Where(p => p.Nome.Contains(searchName)).ToList();
        var matchingPostosDTO = _mockPostosDeVacinacaoDTO.Where(p => p.Nome.Contains(searchName)).ToList();
        _mockRepository.Setup(repo => repo.SearchByName(searchName)).ReturnsAsync(matchingPostos);
        _mockMapper.Setup(mapper => mapper.Map<List<PostoDeVacinacaoDTO>>(matchingPostos)).Returns(matchingPostosDTO);

        // Act
        var result = await _service.SearchByName(searchName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(matchingPostosDTO.Count, result.Count);
        Assert.Equal(matchingPostosDTO, result);
    }

    [Fact]
    public async Task SearchByName_ShouldThrowException_WhenNoMatchingPostosFound()
    {
        // Arrange
        var searchName = "NonExistingPosto";
        _mockRepository.Setup(repo => repo.SearchByName(searchName)).ReturnsAsync((List<PostoDeVacinacao>)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _service.SearchByName(searchName));
        Assert.Equal("Posto não encontrado.", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdatePostoDeVacinacao_WhenIdExists()
    {
        // Arrange
        var existingPostoDTO = _mockPostosDeVacinacaoDTO.First();
        var existingPosto = _mockPostosDeVacinacao.First();
        _mockRepository.Setup(repo => repo.Get(existingPostoDTO.Id)).ReturnsAsync(existingPosto);
        _mockMapper.Setup(mapper => mapper.Map(existingPostoDTO, existingPosto)).Returns(existingPosto);
        _mockRepository.Setup(repo => repo.Update(existingPosto)).ReturnsAsync(existingPosto);
        _mockMapper.Setup(mapper => mapper.Map<PostoDeVacinacaoDTO>(existingPosto)).Returns(existingPostoDTO);

        // Act
        var result = await _service.UpdateAsync(existingPostoDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingPostoDTO, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenIdDoesNotExist()
    {
        // Arrange
        var nonExistingPostoDTO = new PostoDeVacinacaoDTO { Id = 99, Nome = "NonExistingPosto", Endereco = "Endereço 99" };
        _mockRepository.Setup(repo => repo.Get(nonExistingPostoDTO.Id)).ReturnsAsync((PostoDeVacinacao)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(nonExistingPostoDTO));
        Assert.Equal("Posto não encontrado.", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenPostoHasVacinas()
    {
        // Arrange
        long postoId = 1;
        var posto = new PostoDeVacinacao { Id = postoId, Nome = "Posto 1", Endereco = "Endereço 1" };
        var vacinas = new List<Vacina> { new Vacina { Id = 1, Nome = "Vacina 1", PostoDeVacinacaoId = postoId } };

        _mockRepository.Setup(repo => repo.Get(postoId)).ReturnsAsync(posto);
        _mockVacinaRepository.Setup(repo => repo.GetByPostoDeVacinacaoId(postoId)).ReturnsAsync(vacinas);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteAsync(postoId));
        Assert.Equal("Postos de vacinação com vacinas associadas não podem ser excluídos.", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeletePostoDeVacinacao_WhenNoVacinasAreAssociated()
    {
        // Arrange
        var postoId = 1;
        var posto = _mockPostosDeVacinacao.First(p => p.Id == postoId);
        var vacinas = new List<Vacina>();

        _mockRepository.Setup(repo => repo.Get(postoId)).ReturnsAsync(posto);
        _mockVacinaRepository.Setup(repo => repo.GetByPostoDeVacinacaoId(postoId)).ReturnsAsync(vacinas);
        _mockRepository.Setup(repo => repo.Delete(postoId)).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteAsync(postoId);

        // Assert
        _mockRepository.Verify(repo => repo.Delete(postoId), Times.Once);
    }
}
