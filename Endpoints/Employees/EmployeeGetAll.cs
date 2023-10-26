using product_orders.Infrastructure.Data;

namespace product_orders.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        if (page == null || rows == null || page <=0 || rows <=0)
            return Results.BadRequest("Número da página e quantidade de linhas devem ser informadas.");
        if (rows > 10)
            return Results.BadRequest("Limite máximo de linhas é 10.");
        
        try{
            var result = await query.Execute(page.Value, rows.Value);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}
