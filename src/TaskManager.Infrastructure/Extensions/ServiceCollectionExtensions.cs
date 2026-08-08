using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Abstractions;
using TaskManager.Infrastructure.Clock;
using TaskManager.Infrastructure.OpenCode;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.Services;
using TaskManager.Infrastructure.Sse;

namespace TaskManager.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTaskManagerInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagerDbContext>(o => o.UseSqlite(configuration.GetConnectionString("Sqlite")));

        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<IColumnRepository, ColumnRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ILabelRepository, LabelRepository>();
        services.AddScoped<ICardLabelRepository, CardLabelRepository>();
        services.AddScoped<IChecklistRepository, ChecklistRepository>();
        services.AddScoped<IChecklistItemRepository, ChecklistItemRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<ICardSpecRepository, CardSpecRepository>();
        services.AddScoped<IOpenCodeSessionRepository, OpenCodeSessionRepository>();
        services.AddScoped<ISessionEventRepository, SessionEventRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IOpenCodeHost, OpenCodeHostService>();
        services.AddSingleton<ISessionEventChannel, SessionEventChannel>();

        return services;
    }
}
