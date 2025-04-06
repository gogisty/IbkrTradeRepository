using Microsoft.Extensions.Options;
using TradeRepositoryAPI.IbkrClient;

namespace TradeRepositoryAPI.Endpoints
{
    public static class IbkrFlexQueryEndpoints
    {
        public static void MapIbkrFlexQueryEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("flexquery/statements", async (
                string queryNumber,
                IbkrService ibkrService,
                IOptions<IbkrSettings> settings) =>
            {
                var referenceCode = await ibkrService.GetReferenceCode(queryNumber, settings.Value.Token);

                if (referenceCode != null)
                {
                    var response = await ibkrService.GetStatements(referenceCode, settings.Value.Token);

                    if (response != null)
                    {
                        return Results.Ok(response);
                    }
                    else
                    {
                        return Results.Problem("Unable to get FlexQueryResponse");
                    }
                }
                else
                {
                    return Results.Problem("Unable to get ReferenceCode");
                }
            });
        }
    }
}
