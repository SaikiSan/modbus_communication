using System;

namespace ModBusService.Models
{
    public class ModBusAlertMessage : IEquatable<ModBusAlertMessage>
    {
        public ModBusAlertMessage(int address, int value)
        {
            Address = address;
            Value = value;
            BoolValue = true;
        }

        public ModBusAlertMessage(int address, bool value)
        {
            Address = address;
            BoolValue = value;
        }

        public ModBusAlertMessage(int address, double value)
        {
            Address = address;
            FloatValue = value;
        }

        public int Address { get; }
        public int Value { get; set; }
        public bool BoolValue { get; set; }
        public double FloatValue { get; set; }
        public bool Equals(ModBusAlertMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Address == other.Address && Value == other.Value && BoolValue == other.BoolValue && FloatValue == other.FloatValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ModBusAlertMessage)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Address.GetHashCode()) * 397) ^ Value;
            }
        }
    }
}