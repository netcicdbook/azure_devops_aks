using Auditory.API.Data.Context;
using Auditory.API.Models;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using System.Text.Json;

namespace Auditory.API.Features.UserRecordsHistory.GetUserRecordsHistory
{
    public record GetUserRecordsHistoryQuery(string UserName) : IRequest<GetUserRecordsHistoryResult>;
    public record GetUserRecordsHistoryResult(IEnumerable<UserRecordHistory> UserRecordHistory);
    public class GetUserRecordsHistoryHandler(IMapper mapper, IAuditoryContext context, ILogger<GetUserRecordsHistoryHandler> logger) 
                                            : IRequestHandler<GetUserRecordsHistoryQuery, GetUserRecordsHistoryResult>
    {       
        public async Task<GetUserRecordsHistoryResult> Handle(GetUserRecordsHistoryQuery request, CancellationToken cancellationToken)
        {
            var userRecords = await GetUserRecordsHistory(request.UserName,cancellationToken);
            return new GetUserRecordsHistoryResult(userRecords);
        }
        public async Task<IEnumerable<UserRecordHistory>> GetUserRecordsHistory(string userName, CancellationToken cancellationToken)
        {
            logger.LogInformation("Buscar en Mongo registros horarios del empleado:  {userName}", userName);

            var queryResult=await context.UserRecordCollection.FindAsync(a => a.UserName == userName,cancellationToken: cancellationToken);
           
            var result = queryResult.ToListAsync().Result;
            var serializedResults = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            logger.LogInformation("Registros horarios encontrados para el empleado {userName}: \n {results}", userName, serializedResults);

            return mapper.Map<IEnumerable<UserRecordHistory>>(result);           
        }
    }
    public class GetUserRecordsHistoryProfile : Profile
    {
        public GetUserRecordsHistoryProfile()
        {
            // Mapeo entre modelo y respuesta esperada por el cliente
            CreateMap<UserRecordHistory, UserRecordsHistoryResponse>();

            // Mapeo entre result de la API hacia cliente y response de base de datos
            CreateMap<GetUserRecordsHistoryResult, GetUserRecordsHistoryResponse>();                
        }
    }
}
