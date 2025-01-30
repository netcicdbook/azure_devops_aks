using AutoMapper;
using Carter;
using MediatR;

namespace Auditory.API.Features.UserRecordsHistory.GetUserRecordsHistory
{
    public record GetUserRecordsHistoryResponse(IEnumerable<UserRecordsHistoryResponse> UserRecordHistory);
    
    public class GetUserRecordsHistoryEndpoint(IMapper mapper, ILogger<GetUserRecordsHistoryEndpoint> logger) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/UserRecordsHistory/{userName}", async (string userName, ISender sender) =>
            {
                logger.LogInformation("Obtener registros horarios del empleado:  {userName}", userName);
                
                var query = new GetUserRecordsHistoryQuery(userName);
                var result = await sender.Send(query);
                
                logger.LogInformation("Obtenidos {userRecordsCount} registros horarios", result.UserRecordHistory.Count().ToString());

                var response = mapper.Map<GetUserRecordsHistoryResponse>(result);

                return Results.Ok(response.UserRecordHistory);
            }).WithName("GetUserRecordsHistoryByUserName")
            .Produces<GetUserRecordsHistoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Registros horarios de empleado")
            .WithDescription("Obtener lista de registros horarios de empleado");
        }
    }
}
