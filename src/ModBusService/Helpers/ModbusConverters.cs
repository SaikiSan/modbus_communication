using System;

namespace ModBusService.Helpers
;

public class ModbusConverters
{
    public short[] ConvertIntToBit(int value)
    {
        var bitConverted = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian) { Array.Reverse(bitConverted); }
        
        byte[] byteOrder1 = { bitConverted[1], bitConverted[0] };
        short convertedInt1 = BitConverter.ToInt16(byteOrder1, 0);
        
        byte[] byteOrder2 = { bitConverted[3], bitConverted[2] };
        short convertedInt2 = BitConverter.ToInt16(byteOrder2, 0);
        
        short[] result = new short[2];
        result[0] = convertedInt2; result[1] = convertedInt1;

        return result;
    }

    public short[] ConvertFloatToBit(float value)
    {
        var bitConverted = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian) { Array.Reverse(bitConverted); }
        
        byte[] byteOrder1 = { bitConverted[1], bitConverted[0] };
        short convertedInt1 = BitConverter.ToInt16(byteOrder1, 0);
        
        byte[] byteOrder2 = { bitConverted[3], bitConverted[2] };
        short convertedInt2 = BitConverter.ToInt16(byteOrder2, 0);
        
        short[] result = new short[2];
        result[0] = convertedInt2; result[1] = convertedInt1;

        return result;
    }

    public short[] ConvertFloatToBit(double value)
    {
        var bitConverted = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian) { Array.Reverse(bitConverted); }
        
        byte[] byteOrder1 = { bitConverted[1], bitConverted[0] };
        short convertedInt1 = BitConverter.ToInt16(byteOrder1, 0);
        
        byte[] byteOrder2 = { bitConverted[3], bitConverted[2] };
        short convertedInt2 = BitConverter.ToInt16(byteOrder2, 0);
        
        short[] result = new short[2];
        result[0] = convertedInt2; result[1] = convertedInt1;

        return result;
    }

    public double ConvertDW2Float(short int1, short int2)
    {
        byte[] intBytes1 = BitConverter.GetBytes(int1);
        if (BitConverter.IsLittleEndian) Array.Reverse(intBytes1);
        byte[] result1 = intBytes1;
        byte[] intBytes2 = BitConverter.GetBytes(int2);
        if (BitConverter.IsLittleEndian) Array.Reverse(intBytes2);
        byte[] result2 = intBytes2;
        byte[] _bytes = new byte[4];
        _bytes[0] = intBytes1[1];
        _bytes[1] = intBytes1[0];
        _bytes[2] = intBytes2[1];
        _bytes[3] = intBytes2[0];
        double _val = BitConverter.ToSingle(_bytes, 0);
        _val = Math.Round(_val, 3);

        return _val;
    }
}