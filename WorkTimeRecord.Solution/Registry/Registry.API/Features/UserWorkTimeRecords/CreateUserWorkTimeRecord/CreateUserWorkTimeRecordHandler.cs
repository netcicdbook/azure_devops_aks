using AutoMapper;
using Components.Communication.Events;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Registry.API.Data.Context;
using Registry.API.Domain;
namespace Registry.API.Features.UserWorkTimeRecords.CreateUserWorkTimeRecord
{
    public record UserRecordCreatedCommand(string UserName, string FirstName, string LastName, DateTime LastRecord, string Mode) 
                                        : IRequest<UserRecordCreatedResult>;
    public record UserRecordCreatedResult(bool recordPublished);
    public class CreateUserWorkTimeRecordHandler(IMapper mapper, IRegistryContext context, IPublishEndpoint publishEndpoint, 
                                                ILogger<CreateUserWorkTimeRecordHandler> logger) 
        : IRequestHandler<UserRecordCreatedCommand, UserRecordCreatedResult>
    {
        public async Task<UserRecordCreatedResult> Handle(UserRecordCreatedCommand request, 
                                                          CancellationToken cancellationToken) 
        {
            var userRecordEntity=await CreateUserRecord(mapper.Map<UserWorkTimeRecord>(request),cancellationToken);
            
            await PublishCreatedUserRecordEvent(mapper.Map<UserWorkTimeRecordCreatedEvent>(userRecordEntity), cancellationToken);

            return new UserRecordCreatedResult(true);
        }
        public async Task<UserWorkTimeRecord> CreateUserRecord(UserWorkTimeRecord userRecord, 
                                                              CancellationToken cancellationToken)
        {
            var existingRecord = await context.UserWorkTimeRecords.FirstOrDefaultAsync(u => u.UserName == userRecord.UserName, cancellationToken);
            

            if (existingRecord == null) context.UserWorkTimeRecords.Add(userRecord);
            else {
                existingRecord.LastRecord = userRecord.LastRecord;
                existingRecord.Mode = userRecord.Mode;
            }
            await context.SaveChangesAsync();

            logger.LogInformation("Persistido en PostgreSQL: {UserName}", userRecord.UserName);

            return userRecord;
        }
        public async Task<bool> PublishCreatedUserRecordEvent(UserWorkTimeRecordCreatedEvent userWorkTimeRecordCreatedEvent, 
                                                              CancellationToken cancellationToken) {            
            await publishEndpoint.Publish(userWorkTimeRecordCreatedEvent, cancellationToken);

            return true;
        }
    }
    public class CreateUserWorkTimeRecordProfile : Profile
    {
        public CreateUserWorkTimeRecordProfile()
        {
            //Mapear de Command a entidad del dominio
            CreateMap<UserRecordCreatedCommand, UserWorkTimeRecord>();
            //Mapear de request a command
            CreateMap<UserRecordCreatedRequest, UserRecordCreatedCommand>().ReverseMap();
            //Mapear de Dominio a Evento
            CreateMap<UserWorkTimeRecord, UserWorkTimeRecordCreatedEvent>();
            // Mapeo entre result de la API hacia cliente y response de base de datos
            CreateMap<UserRecordCreatedResult, UserRecordCreatedResponse>().ReverseMap();
        }
    }
}
