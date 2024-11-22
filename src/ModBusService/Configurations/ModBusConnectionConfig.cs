using System.IO.Ports;

namespace ModBusService.Configurations
{
    public class ModBusConnectionConfig
    {
        public ConnectionType ConnectionType { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string COMName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }
    }
}