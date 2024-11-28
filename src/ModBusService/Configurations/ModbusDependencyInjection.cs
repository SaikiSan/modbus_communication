using Microsoft.Extensions.DependencyInjection;
using ModBusService.Communication;
using ModBusService.Communications;
using ModBusService.Connections;
using ModBusService.Factories;
using ModBusService.Helpers;

namespace ModBusService.Configurations
{
    public static class ModbusDependencyInjection
    {
        public static IServiceCollection AddModbus(this IServiceCollection services)
        {
            services.AddTransient<ModbusConverters>();
            services.AddSingleton<IModbusCommands, ModbusCommands>();
            services.AddSingleton<ModBusTCPCommunication>();
            services.AddSingleton<ModBusRTUCommunication>();
            services.AddSingleton<IModbusCommunicationFactory, ModbusCommunicationFactory>();
            services.AddSingleton<IModbusConnection, ModbusConnection>();

            return services;
        }
    }
}