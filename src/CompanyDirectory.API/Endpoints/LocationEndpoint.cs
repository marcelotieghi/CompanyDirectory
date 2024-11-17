using Carter;
using CompanyDirectory.UseCases.Location.Create;
using CompanyDirectory.UseCases.Location.GetAll;
using CompanyDirectory.UseCases.Location.GetById;
using CompanyDirectory.UseCases.Location.SoftDelete;
using CompanyDirectory.UseCases.Location.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDirectory.API.Endpoints;

public class LocationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/locations", GetAllLocations)
            .WithName("GetAllLocations")
            .Produces(200)
            .Produces(400)
            .WithOpenApi();

        app.MapGet("/locations/{id:int}", GetLocationById)
            .WithName("GetLocationById")
            .Produces(200)
            .Produces(404)
            .WithOpenApi();

        app.MapPost("/locations", CreateLocation)
           .WithName("CreateLocation")
           .Produces(201)
           .Produces(400)
           .WithOpenApi();

        app.MapPut("/locations/{id:int}", UpdateLocation)
           .WithName("UpdateLocation")
           .Produces(200)
           .Produces(400)
           .WithOpenApi();

        app.MapPut("/locations/delete", SoftDeleteLocation)
           .WithName("SoftDeleteLocation")
           .Produces(200)
           .Produces(400)
           .WithOpenApi();
    }

    private static async Task<IResult> GetAllLocations(
        ISender sender)
    {
        try
        {
            var result = await sender.Send(new GetAllLocationRequest());
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> GetLocationById(
        ISender sender,
        int id)
    {
        try
        {
            var result = await sender.Send(new GetByIdLocationRequest(id));
            return result.IsSuccess
                ? TypedResults.Ok(result.Value) 
                : TypedResults.NotFound(result.Errors);

        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> CreateLocation(
         ISender sender,
        [FromBody] CreateLocationRequest request)
    {
        try
        {
            var result = await sender.Send(request);
            return result.IsSuccess
                ? TypedResults.Created(result.ToString()) 
                : TypedResults.BadRequest(result.Errors); 
        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdateLocation(
        ISender sender,
        int id,
        [FromBody] UpdateLocationRequest request)
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

    private static async Task<IResult> SoftDeleteLocation(
        ISender sender,
        [FromBody] SoftDeleteLocationRequest request)
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