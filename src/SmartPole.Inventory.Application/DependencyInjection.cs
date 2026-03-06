using Microsoft.Extensions.DependencyInjection;

namespace SmartPole.Inventory.Application;

public static class DependencyInjection {
  public static IServiceCollection AddApplication(this IServiceCollection services) {
    services.AddMediatR(cfg => {
      cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
    });

    services.AddSingleton<SmartPole.Inventory.Domain.ML.IYoloModelService, SmartPole.Inventory.Application.ML.YoloModelService>();

    return services;
  }
}
