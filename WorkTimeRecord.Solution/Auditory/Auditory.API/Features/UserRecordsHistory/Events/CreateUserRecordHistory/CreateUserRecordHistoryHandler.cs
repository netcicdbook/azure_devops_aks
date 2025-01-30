using Auditory.API.Data;
using Auditory.API.Data.Context;
using Auditory.API.Models;
using AutoMapper;
using Components.Communication.Events;
using MassTransit;
using MediatR;
using System.Text.Json;

namespace Auditory.API.Features.UserRecordsHistory.Events.CreateUserRecordHistory
{
    public record CreateUserRecordHistoryCreatedCommand(UserWorkTimeRecordCreatedEventDto UserWorkTimeRecordCreatedEventDto) 
        : IRequest<CreateUserRecordHistoryCreatedResult>;
    public record CreateUserRecordHistoryCreatedResult(string Id);

    public class CreateUserRecordHistoryHandler(ISender sender, IMapper mapper, 
                                                ILogger<CreateUserRecordHistoryHandler> logger) 
        : IConsumer<UserWorkTimeRecordCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserWorkTimeRecordCreatedEvent> context)
        {
            var eventMessage = context.Message;
            logger.LogInformation("Consumiendo evento 'CreateUserRecordHistoryCreatedEvent':\n    EventId: {EventId}\n    " +
                                  "EventType: {EventType}", eventMessage.Id, eventMessage.EventType);

            var command = new CreateUserRecordHistoryCreatedCommand(mapper.Map<UserWorkTimeRecordCreatedEventDto>(eventMessage));
            logger.LogInformation("Enviando comando CreateUserRecordHistoryCreatedCommand con el contenido " +
                                  "del mensaje:\n    MessageContent: {MessageContent}", eventMessage);

            await sender.Send(command);
        }
    }

    public class CreateUserRecordHistoryCreatedCommandHandler(IMapper mapper, IAuditoryContext context, 
                                                              ILogger<CreateUserRecordHistoryCreatedCommandHandler> logger) 
        : IRequestHandler<CreateUserRecordHistoryCreatedCommand, CreateUserRecordHistoryCreatedResult>
    {
        public async Task<CreateUserRecordHistoryCreatedResult> Handle(CreateUserRecordHistoryCreatedCommand request
                                                                     , CancellationToken cancellationToken)
        {
            var userWorkTimeRecordCreatedEventDto = request.UserWorkTimeRecordCreatedEventDto;
            var serializedDto = JsonSerializer.Serialize(userWorkTimeRecordCreatedEventDto, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            logger.LogInformation("Se va a persitir en Mongo 'UserRecord':\n  {Entidad}", serializedDto);

            var userRecord = mapper.Map<UserRecordHistory>(userWorkTimeRecordCreatedEventDto);
            var userRecordMongo = mapper.Map<UserRecordHistoryMongo>(userRecord);

            var id = await CreateUserRecordHistoryRecord(userRecordMongo, cancellationToken);

            logger.LogInformation("Entidad UserRecordHistory persitida en Mongo con el Id: {DocumentId}", id);

            return new CreateUserRecordHistoryCreatedResult(id);
        }
        public async Task<string> CreateUserRecordHistoryRecord(UserRecordHistoryMongo userRecord
                                                              , CancellationToken cancellationToken)
        {
            await context.UserRecordCollection.InsertOneAsync(userRecord, cancellationToken: cancellationToken);
            return userRecord.Id;
        }
    }

    public class CreateUserRecordHistoryProfile : Profile
    {
        public CreateUserRecordHistoryProfile()
        {
            //Mapeo de DTO a Modelo del dominio
            CreateMap<UserRecordHistory, UserWorkTimeRecordCreatedEventDto>().ReverseMap();

            //Mapeo de Evento a DTO
            CreateMap<UserWorkTimeRecordCreatedEvent, UserWorkTimeRecordCreatedEventDto>();
            
        }
    }
}
