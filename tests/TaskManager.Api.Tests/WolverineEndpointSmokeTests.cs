using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Application.Dtos;

namespace TaskManager.Api.Tests;

public sealed class WolverineEndpointSmokeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WolverineEndpointSmokeTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_boards_returns_200_with_empty_array()
    {
        var response = await _client.GetAsync("/api/boards");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<BoardDto[]>();
        Assert.NotNull(body);
        Assert.Empty(body!);
    }

    [Fact]
    public async Task Get_board_by_id_returns_404_for_unknown()
    {
        var response = await _client.GetAsync("/api/boards/123");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_board_returns_404_when_handler_returns_default()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/boards",
            new { name = "Test", description = (string?)null });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Get_card_returns_404_for_unknown()
    {
        var response = await _client.GetAsync("/api/cards/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task List_cards_by_column_returns_200_with_empty_array()
    {
        var response = await _client.GetAsync("/api/columns/1/cards");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<CardDto[]>();
        Assert.NotNull(body);
        Assert.Empty(body!);
    }

    [Fact]
    public async Task Move_column_via_PUT_returns_204()
    {
        var response = await _client.PutAsJsonAsync(
            "/api/boards/1/columns/2/position",
            new { boardId = 0L, columnId = 0L, newPosition = 3 });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_board_returns_204()
    {
        var response = await _client.DeleteAsync("/api/boards/77");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task SSE_events_stream_returns_text_event_stream_content_type()
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        var response = await _client.GetAsync(
            "/api/sessions/42/events/stream",
            HttpCompletionOption.ResponseHeadersRead,
            cts.Token);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("text/event-stream", response.Content.Headers.ContentType?.MediaType);

        response.Dispose();
    }
}
