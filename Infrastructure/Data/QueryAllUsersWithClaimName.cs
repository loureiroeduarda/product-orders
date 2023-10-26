using Dapper;
using Microsoft.Data.SqlClient;
using product_orders.Endpoints.Employees;

namespace product_orders.Infrastructure.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(DotNetEnv.Env.GetString("DB_LOCAL"));
        var query =
            @"select Email, ClaimValue as Name
            from AspNetUsers u inner
            join AspNetUserClaims c
            on u.id = UserId and ClaimType = 'Name'
            order by name
            OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
    }
}
