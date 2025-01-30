using AutoMapper;
using Carter;
using MediatR;

namespace Registry.API.Features.UserWorkTimeRecords.GetUserWorkTimeRecord
{
    public record GetUserWorkTimeRecordResponse(UserWorkTimeRecordResponse UserWorkTimeRecord);    
    public class GetUserWorkTimeRecordEndpoint(IMapper mapper, ILogger<GetUserWorkTimeRecordEndpoint> logger) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/UserWorkTimeRecord/{userName}", async (string userName, ISender sender) =>
            {
                logger.LogInformation("Obtener registro horario del empleado:  {userName}", userName);

                var query = new GetUserWorkTimeRecordQuery(userName);
                var result = await sender.Send(query);

                logger.LogInformation("Obtenidos {UserWorkTimeRecord} registros horarios", result.UserWorkTimeRecord);

                var response = mapper.Map<GetUserWorkTimeRecordResponse>(result);
                
                return Results.Ok(response.UserWorkTimeRecord);
            }).WithName("GetUserWorkTimeRecordByUserName")
            .Produces<GetUserWorkTimeRecordResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Registro horario del empleado")
            .WithDescription("Obtener el último registro horario del empleado");
        }
    }
}

