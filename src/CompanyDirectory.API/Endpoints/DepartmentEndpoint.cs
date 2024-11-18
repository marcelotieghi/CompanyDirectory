using Carter;
using CompanyDirectory.UseCases.Department.Create;
using CompanyDirectory.UseCases.Department.GetAll;
using CompanyDirectory.UseCases.Department.GetById;
using CompanyDirectory.UseCases.Department.SoftDelete;
using CompanyDirectory.UseCases.Department.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDirectory.API.Endpoints;

public sealed class DepartmentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/departments", GetAllDepartment)
            .WithName("GetAllDepartment")
            .Produces(200)
            .Produces(400)
            .WithOpenApi();

        app.MapGet("/departments/{id:int}", GetByIdDepartment)
            .WithName("GetByIdDepartment")
            .Produces(200)
            .Produces(404)
            .WithOpenApi();

        app.MapPost("/departments", CreateDepartment)
           .WithName("CreateDepartment")
           .Produces(201)
           .Produces(400)
           .WithOpenApi();

        app.MapPut("/departments/{id:int}", UpdateDepartment)
           .WithName("UpdateDepartment")
           .Produces(200)
           .Produces(400)
           .WithOpenApi();

        app.MapPut("/departments/delete", SoftDeleteDepartment)
           .WithName("SoftDeleteDepartment")
           .Produces(200)
           .Produces(400)
           .WithOpenApi();
    }

    private static async Task<IResult> GetAllDepartment(
        ISender sender)
    {
        try
        {
            var result = await sender.Send(new GetAllDepartmentRequest());
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> GetByIdDepartment(
        ISender sender,
        int id)
    {
        try
        {
            var result = await sender.Send(new GetByIdDepartmentRequest(id));
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> CreateDepartment(
        ISender sender,
        [FromBody] CreateDepartmentRequest request)
    {
        try
        {
            var result = await sender.Send(request);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdateDepartment(
        ISender sender,
        int id,
        [FromBody] UpdateDepartmentRequest request)
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
        catch (Exception ex)
        {
            return TypedResults.Problem($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<IResult> SoftDeleteDepartment(
        ISender sender,
        [FromBody] SoftDeleteDepartmentRequest request)
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