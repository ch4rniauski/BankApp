using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using ch4rniauski.BankApp.Authentication.Domain.Entities;
using ch4rniauski.BankApp.Authentication.Tests.Helpers.DataProviders;
using Moq;
using Xunit;

namespace ch4rniauski.BankApp.Authentication.Tests.Tests.UnitTests.Client;

public sealed class DeleteClientUnitTests
{
    private readonly DeleteClientCommandHandler _commandHandler;
    private readonly Mock<IClientRepository> _clientRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public DeleteClientUnitTests()
    {
        _commandHandler = new DeleteClientCommandHandler(_clientRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task DeleteClient_ReturnsSuccess()
    {
        // Arrange
        var command = new DeleteClientCommand(Guid.NewGuid());
        var returnedClient = new ClientEntity();
        var responseDto = ClientDataProvider.GenerateDeleteClientResponseDto();
        
        const bool isSuccess = true;

        _clientRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(returnedClient);

        _clientRepositoryMock.Setup(r => r.DeleteWithAttachmentAsync(returnedClient, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _mapperMock.Setup(m => m.Map<DeleteClientResponseDto>(returnedClient))
            .Returns(responseDto);
        
        // Act
        var response = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(isSuccess, response.IsSuccess);
        Assert.Equal(responseDto, response.Value);
        Assert.NotNull(response.Value);
        Assert.Null(response.Error);
    }
}
