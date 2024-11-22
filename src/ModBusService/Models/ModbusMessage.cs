namespace ModBusService.Models
{
    public class ModbusMessage
    {
        public ModbusMessage(int address, short value)
        {
            Address = address;
            Value = value;
        }

        public int Address { get; set; }
        public short Value { get; set; }
    }
}