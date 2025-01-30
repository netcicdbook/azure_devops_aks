using AutoMapper;
using Carter;
using MediatR;

namespace Registry.API.Features.UserWorkTimeRecords.CreateUserWorkTimeRecord
{
    public record UserRecordCreatedRequest(string UserName, string FirstName, string LastName, DateTime LastRecord, string Mode);
    public record UserRecordCreatedResponse(bool recordPublished);
    public class CreateUserWorkTimeRecordEndpoint(IMapper mapper) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/UserWorkTimeRecord", async (UserRecordCreatedRequest request, ISender sender) =>            
            {
                var command = mapper.Map<UserRecordCreatedCommand>(request);
                var result = await sender.Send(command);
                var response = mapper.Map<UserRecordCreatedResponse>(result);
                return Results.Ok(response);
            }).WithName("UserWorkTimeRecordCreated")
            .Produces<UserRecordCreatedResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Crear nuevo registro horario de empleado")
            .WithDescription("Crear nuevo registro horario de empleado");
        }
    }
}
