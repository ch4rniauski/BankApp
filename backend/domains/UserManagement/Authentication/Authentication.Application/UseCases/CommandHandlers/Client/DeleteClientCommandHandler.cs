using AutoMapper;
using ch4rniauski.BankApp.Authentication.Application.Contracts.Repositories;
using ch4rniauski.BankApp.Authentication.Application.DTO.Client.Responses;
using ch4rniauski.BankApp.Authentication.Application.UseCases.Commands.Client;
using MediatR;

namespace ch4rniauski.BankApp.Authentication.Application.UseCases.CommandHandlers.Client;

public sealed class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, DeleteClientResponseDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<DeleteClientResponseDto> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var doesExist = await _clientRepository.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new Exception("User with given id does not exist.");
        
        var isDeleted = await _clientRepository.DeleteWithAttachmentAsync(doesExist, cancellationToken);

        if (!isDeleted)
        {
            throw new Exception("User wasn't deleted.");
        }
        
        return _mapper.Map<DeleteClientResponseDto>(doesExist);
    }
}
