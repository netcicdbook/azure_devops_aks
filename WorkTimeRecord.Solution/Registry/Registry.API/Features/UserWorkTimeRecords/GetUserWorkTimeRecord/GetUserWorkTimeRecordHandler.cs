using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Registry.API.Data.Context;
using Registry.API.Domain;
using System.Text.Json;

namespace Registry.API.Features.UserWorkTimeRecords.GetUserWorkTimeRecord
{
    public record GetUserWorkTimeRecordQuery(string UserName) : IRequest<GetUserWorkTimeRecordResult>;
    public record GetUserWorkTimeRecordResult(UserWorkTimeRecord UserWorkTimeRecord);
    public class GetUserWorkTimeRecordHandler(IRegistryContext context, ILogger<GetUserWorkTimeRecordHandler> logger) 
                                            : IRequestHandler<GetUserWorkTimeRecordQuery, GetUserWorkTimeRecordResult>
    {       
        public async Task<GetUserWorkTimeRecordResult> Handle(GetUserWorkTimeRecordQuery request, CancellationToken cancellationToken)
        {
            var userWorkTimeRecord = await GetUserWorkTimeRecord(request.UserName,cancellationToken);
            return new GetUserWorkTimeRecordResult(userWorkTimeRecord);
        }        
        public async Task<UserWorkTimeRecord> GetUserWorkTimeRecord(string userName, CancellationToken cancellationToken)
        {
            logger.LogInformation("Buscar en Postgresql último registro horario del empleado:  {userName}", userName);

            var userWorkTimeRecord = await context.UserWorkTimeRecords.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
            var serializedResult = JsonSerializer.Serialize(userWorkTimeRecord, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            if (userWorkTimeRecord == null)
            {
                logger.LogInformation("No existe el registro en PostgreSQL: {UserName}", userName);
                userWorkTimeRecord = new UserWorkTimeRecord();
            }
            else
                logger.LogInformation("Obtenido de PostgreSQL {UserName}: \n {serializedResult}", userWorkTimeRecord.UserName, serializedResult);            

            return userWorkTimeRecord;
        }
    }
    public class GetUserWorkTimeRecordProfile : Profile
    {
        public GetUserWorkTimeRecordProfile()
        {
            // Mapeo entre modelo y respuesta esperada por el cliente
            CreateMap<UserWorkTimeRecord, UserWorkTimeRecordResponse>();

            // Mapeo entre result de la API hacia cliente y response de base de datos
            CreateMap<GetUserWorkTimeRecordResult, GetUserWorkTimeRecordResponse>();
        }
    }
}
