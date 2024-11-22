using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HslCommunication;

namespace ModBusService.Communications;

public interface IModbusCommunication
{
    Task TryConnectAsync();
    Task<OperateResult> WriteAsync(string address, byte[] value);
    Task<OperateResult> WriteAsync(string address, short[] value);
    Task<OperateResult> WriteAsync(string address, short value);
    Task<OperateResult> WriteAsync(string address, bool value);
    Task<OperateResult> WriteAsync(string address, bool[] value);
    Task<OperateResult<short[]>> ReadInt16Async(string address, ushort length);
    Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length);
}