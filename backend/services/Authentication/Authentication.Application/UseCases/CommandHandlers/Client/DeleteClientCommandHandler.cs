using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Common.Errors;
using ch4rniauski.BankApp.Authentication.Application.Common.Results;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<DeleteClientResponseDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<Result<DeleteClientResponseDto>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var doesExist = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (doesExist is null)
        {
            return Result<DeleteClientResponseDto>
                .Failure(Error.NotFound(
                    $"Client with ID {request.Id} does not exist"
                    ));
        }
        
        var isDeleted = await _clientRepository.DeleteWithAttachmentAsync(doesExist, cancellationToken);

        if (!isDeleted)
        {
            return Result<DeleteClientResponseDto>
                .Failure(Error.InternalError(
                    $"Client with ID {request.Id} was not deleted"
                ));
        }
        
        var response = _mapper.Map<DeleteClientResponseDto>(doesExist);
        
        return Result<DeleteClientResponseDto>.Success(response);
    }
}
