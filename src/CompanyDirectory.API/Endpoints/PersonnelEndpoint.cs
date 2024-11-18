using Carter;
using CompanyDirectory.UseCases.Personnel.Create;
using CompanyDirectory.UseCases.Personnel.GetAll;
using CompanyDirectory.UseCases.Personnel.GetById;
using CompanyDirectory.UseCases.Personnel.SoftDelete;
using CompanyDirectory.UseCases.Personnel.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDirectory.API.Endpoints;

public sealed class PersonnelEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/personnel", GetAllPersonnel)
            .WithName("GetAllPersonnel")
            .Produces(200)
            .Produces(400)
            .WithOpenApi();

        app.MapGet("/personnel/{id:int}", GetByIdPersonnel)
            .WithName("GetByIdPersonnel")
            .Produces(200)
            .Produces(404)
            .WithOpenApi();

        app.MapPost("/personnel", CreatePersonnel)
           .WithName("CreatePersonnel")
           .Produces(201)
           .Produces(400)
           .WithOpenApi();

        app.MapPut("/personnel/{id:int}", UpdatePersonnel)
           .WithName("UpdatePersonnel")
           .Produces(200)
           .Produces(400)
           .WithOpenApi();

        app.MapPut("/personnel/delete", SoftDeletePersonnel)
           .WithName("SoftDeletePersonnel")
           .Produces(200)
           .Produces(400)
           .WithOpenApi();
    }

    private static async Task<IResult> GetAllPersonnel(
        ISender sender)
    {
        try
        {
            var result = await sender.Send(new GetAllPersonnelRequest());
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch(Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> GetByIdPersonnel(
        ISender sender,
        int id)
    {
        try
        {
            var result = await sender.Send(new GetByIdPersonnelRequest(id));
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch(Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> CreatePersonnel(
        ISender sender,
        [FromBody] CreatePersonnelRequest request)
    {
        try
        {
            var result = await sender.Send(request);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch(Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdatePersonnel(
        ISender sender,
        int id,
        [FromBody] UpdatePersonnelRequest request)
    {
        try
        {
            if(id != request.Id)
                return TypedResults.BadRequest("DO TO");

            var result = await sender.Send(request);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch(Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> SoftDeletePersonnel(
        ISender sender,
        [FromBody] SoftDeletePersonnelRequest request)
    {
        try
        {
            var result = await sender.Send(request);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch(Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }
}