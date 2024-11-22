using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Polly;
using System.Net.Sockets;
using System.IO;
using HslCommunication.ModBus;
using ModBusService.Configurations;

namespace ModBusService.Communications
{
    public class ModBusTCPCommunication : ModbusTcpNet, IModbusCommunication
    {
        
        private readonly Policy _policyModBus;
        public ModBusTCPCommunication(IOptions<ModBusConnectionConfig> connection)
        {
            _policyModBus = Policy.Handle<SocketException>()
                .Or<IOException>().WaitAndRetry(new[]
                {
                    TimeSpan.FromMilliseconds(500),
                    TimeSpan.FromMilliseconds(500),
                    TimeSpan.FromMilliseconds(500)
                });
            IpAddress = connection.Value.Ip;
            Port = connection.Value.Port;
        }

        public async Task TryConnectAsync()
        {
            if(!string.IsNullOrEmpty(ConnectionId))
                return;
            Console.WriteLine(ConnectionId);
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