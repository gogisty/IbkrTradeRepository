using System.Text.Json.Serialization;
using TradeRepositoryAPI;
using TradeRepositoryAPI.Endpoints;
using TradeRepositoryAPI.IbkrResponses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<IbkrSettings>()
    .Bind(builder.Configuration.GetSection("IbkrSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddHttpClient<IbkrService>((serviceProvider, httpClient) =>
{
    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
    httpClient.BaseAddress = new Uri("https://ndcdyn.interactivebrokers.com/AccountManagement/FlexWebService/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(5)
    };
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIbkrFlexQueryEndpoints();

app.Run();

[JsonSerializable(typeof(Trade[]))]
[JsonSerializable(typeof(Trades[]))]
[JsonSerializable(typeof(OptionEAE[]))]
[JsonSerializable(typeof(FlexQueryResponse[]))]
[JsonSerializable(typeof(FlexStatement[]))]
[JsonSerializable(typeof(FlexStatements[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}