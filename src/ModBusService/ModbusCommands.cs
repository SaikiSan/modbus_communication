using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentModbus;
using ModBusService.Communications;
using ModBusService.Connections;
using ModBusService.Helpers;
using ModBusService.Models;
using Polly;
using Polly.Fallback;
using Polly.Retry;

namespace ModBusService
{
    public class ModbusCommands : IModbusCommands
    {
        private readonly IModbusCommunication _modBusCommunication;
        private readonly ModbusConverters _modbusConverters;
        private readonly AsyncRetryPolicy<bool> _modbusPolicy;
        public ModbusCommands(IModbusConnection modBusConnection, ModbusConverters modbusConverters)
        {
            _modBusCommunication = modBusConnection.GetConnection();
            _modbusConverters = modbusConverters;

            _modbusPolicy = Policy.HandleResult(false).RetryAsync(4, onRetry: async (outcome, retryNumber, context) =>
            {
                await _modBusCommunication.TryConnectAsync();
            });
        }
        public async Task SendValue(ModbusMessage message)
        {
            await _modBusCommunication.TryConnectAsync();
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(message.Address.ToString(), message.Value);

                return communicationResult.IsSuccess;
            });

        }

        public async Task SendValues(int initialAddress, List<short> values)
        {
            await _modBusCommunication.TryConnectAsync();
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(initialAddress.ToString(), values.ToArray());
                return communicationResult.IsSuccess;
            });
        }

        public async Task SendFloat(int address, float value)
        {
            await _modBusCommunication.TryConnectAsync();
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(address.ToString(), _modbusConverters.ConvertFloatToBit(value));
                return communicationResult.IsSuccess;
            });
        }

        public async Task SendFloatList(int address, List<float> values)
        {
            await _modBusCommunication.TryConnectAsync();
            var byteList = new List<short>();
            foreach (var value in values)
            {
                var newValue = _modbusConverters.ConvertFloatToBit(value);
                byteList.Add(newValue[0]);
                byteList.Add(newValue[1]);
            }

            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(address.ToString(),byteList.ToArray());
                return communicationResult.IsSuccess;
            });
        }

        public async Task SendInt(string address, int value)
        {
            await _modBusCommunication.TryConnectAsync();
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(address, _modbusConverters.ConvertIntToBit(value));
                return communicationResult.IsSuccess;
            });
        }
    
        public async Task SendBool(string address, bool value)
        {
            await _modBusCommunication.TryConnectAsync();
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(address, value);
                return communicationResult.IsSuccess;
            });
        }

        public async Task SendBoolList(int address,  List<bool> values)
        {
            await _modBusCommunication.TryConnectAsync();
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.WriteAsync(address.ToString(), values.ToArray());
                return communicationResult.IsSuccess;
            });
        }

        public async Task<short> ReceiveValue(int address)
        {   
            await _modBusCommunication.TryConnectAsync();
            var result = (short)0;
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.ReadInt16Async(address.ToString(), 1);
                if(communicationResult.IsSuccess)
                    result = communicationResult.Content[0];
                return communicationResult.IsSuccess;
            });
            return result; 
        }

        public async Task<double> ReadFloat(int address)
        {   
            await _modBusCommunication.TryConnectAsync();
            var result = new short[2];

            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.ReadInt16Async(address.ToString(), 2);
                if(communicationResult.IsSuccess)
                    result = communicationResult.Content;
                return communicationResult.IsSuccess;
            });

            return _modbusConverters.ConvertDW2Float(result[0], result[1]);
        }

        public async Task<bool> ReadBool(int address)
        {
            await _modBusCommunication.TryConnectAsync();
            var result = new bool[2];

            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.ReadBoolAsync(address.ToString(), 1);
                if(communicationResult.IsSuccess)
                    result = communicationResult.Content;
                return communicationResult.IsSuccess;
            });
            
            return result[0];
        }

        public async Task<List<ModbusMessage>> ReceiveValues(int initialAddress, int endAddress)
        {
            await _modBusCommunication.TryConnectAsync();
            var values = new List<ModbusMessage>();
            
            await _modbusPolicy.ExecuteAsync(async () => 
            {
                var communicationResult = await _modBusCommunication.ReadInt16Async(initialAddress.ToString(), (ushort)((ushort)(endAddress - initialAddress) + 2));
                if(communicationResult.IsSuccess)
                {
                    foreach (var item in communicationResult.Content)
                    {
                        values.Add(new ModbusMessage(initialAddress, item));
                        initialAddress++;
                    }
                }
                
                return communicationResult.IsSuccess;
            });

            return values;
        }
    }
}
