using Microsoft.Extensions.Options;
using ModBusService.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Polly;
using System.Net.Sockets;
using System.IO;
using HslCommunication.ModBus;
using ModBusService.Communications;
using ModBusService.Configurations;

namespace ModBusService.Communication
{
    public class ModBusRTUCommunication : ModbusTcpNet, IModbusCommunication
    {
        
        private readonly Policy _policyModBus;
        public ModBusRTUCommunication(IOptions<ModBusConnectionConfig> connection)
        {
            
        }

        public async Task TryConnectAsync()
        {
            if(!string.IsNullOrEmpty(ConnectionId))
                return;
            try
            {
                await _policyModBus.Execute(ConnectServerAsync);
            }
            catch (Exception){
            }
        }

        public async Task Disconnect()
        {
            await ConnectCloseAsync();
        }
    }
}