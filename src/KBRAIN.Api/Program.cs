using KBRAIN.Api.Contracts;
using KBRAIN.Application;
using KBRAIN.Application.Chat;
using KBRAIN.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/chat", async (AskRequest request, ChatService chatService, CancellationToken cancellationToken) =>
{
    var answer = await chatService.AskAsync(request.Question, cancellationToken);
    return TypedResults.Ok(new AskResponse(answer));
});

app.MapPost("/api/chat/stream", (AskRequest request, ChatService chatService, CancellationToken cancellationToken) =>
{
    var answer = chatService.StreamAsync(request.Question, cancellationToken);
    return TypedResults.ServerSentEvents(answer);
});

app.Run();
