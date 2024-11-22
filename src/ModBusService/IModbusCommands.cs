using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModBusService
{
    public interface IModbusCommands
    {
        Task SendValue(string address, short value);
        Task SendBool(string address, bool value);
        Task SendFloat(int address, float values);
        Task SendFloatList(int address, List<float> value);
        Task SendBoolList(int address, List<bool> values);
        Task SendInt(string address, int value);
        Task SendValues(int initialAddress, List<short> values);
        Task<short> ReceiveValue(int address);
        Task<double> ReadFloat(int address);
        Task<bool> ReadBool(int address);
        Task<Dictionary<int, int>> ReceiveValues(int initialAddress, int EndAddress);
    }
}