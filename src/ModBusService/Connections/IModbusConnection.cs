using ModBusService.Communications;

namespace ModBusService.Connections;

public interface IModbusConnection
{
    IModbusCommunication GetConnection();
}