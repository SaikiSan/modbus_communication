using Microsoft.Extensions.Options;
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
    public class ModBusRTUCommunication : ModbusRtu, IModbusCommunication
    {
        
        private readonly Policy _policyModBus;
        public ModBusRTUCommunication(IOptions<ModBusConnectionConfig> connection)
        {
            _policyModBus = Policy.Handle<SocketException>()
                .Or<IOException>().WaitAndRetry(new[]
                {
                    TimeSpan.FromMilliseconds(500),
                    TimeSpan.FromMilliseconds(500),
                    TimeSpan.FromMilliseconds(500)
                });

            SerialPortInni(connection.Value.COMName
                            , connection.Value.BaudRate
                            , connection.Value.DataBits
                            , connection.Value.StopBits
                            , connection.Value.Parity);
        }

        public async Task TryConnectAsync()
        {
            if(!string.IsNullOrEmpty(ConnectionId))
                return;
            try
            {
                await _policyModBus.Execute(TryConnectAsync);
            }
            catch (Exception){
            }
        }

        public async Task Disconnect()
        {
            await Task.Yield();
            Close();
        }
    }
}