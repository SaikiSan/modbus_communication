using System;
using ModBusService.Communication;
using ModBusService.Communications;
using ModBusService.Configurations;

namespace ModBusService.Factories;

public class ModbusCommunicationFactory : IModbusCommunicationFactory
{
    private readonly ModBusTCPCommunication _tcpCommunication;
    private readonly ModBusRTUCommunication _rtuCommunication;

    public ModbusCommunicationFactory(ModBusTCPCommunication tcpCommunication, ModBusRTUCommunication rtuCommunication)
    {
        _tcpCommunication = tcpCommunication;
        _rtuCommunication = rtuCommunication;
    }

    public IModbusCommunication Create(ConnectionType connectionType)
    {
        return connectionType switch
        {
            ConnectionType.RTU => _rtuCommunication,
            ConnectionType.TCP => _tcpCommunication,
            _ => throw new ArgumentOutOfRangeException(nameof(connectionType), "Unsupported connection type")
        };
    }
}