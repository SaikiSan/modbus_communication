using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModBusService.Communications;
using ModBusService.Configurations;

namespace ModBusService.Factories;

public interface IModbusCommunicationFactory
{
    IModbusCommunication Create(ConnectionType connectionType);
}
