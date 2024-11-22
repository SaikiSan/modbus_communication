using System;
using Microsoft.Extensions.Options;
using ModBusService.Communications;
using ModBusService.Factories;
using ModBusService.Configurations;

namespace ModBusService.Connections;

public class ModbusConnection : IModbusConnection
{
    private readonly IModbusCommunicationFactory _factory;
    private readonly IOptions<ModBusConnectionConfig> _connection;

    public ModbusConnection(IModbusCommunicationFactory factory, IOptions<ModBusConnectionConfig> connection)
    {
        _factory = factory;
        _connection = connection;
    }

    public IModbusCommunication GetConnection()
    {
        return _factory.Create(_connection.Value.ConnectionType);
    }
}