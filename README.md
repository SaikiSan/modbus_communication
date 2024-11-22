# Modbus .Net Communication

This application demonstrates how to establish a connection with a PLC using both **TCP** and **RTU** connection types.

It leverages **Factory Design Pattern** and **Dependency Injection** for connection management, and uses **Polly** to ensure communication stability by implementing retries in case of failures.

---

## Features

- **Supports TCP and RTU Modbus connections**.
- **Dependency Injection** for seamless integration into your project.
- **Polly integration** for reliable and fault-tolerant communication.
- **Easily configurable** connection settings via `appsettings.json`.

---

## Getting Started

### Prerequisites

1. Add the project to your solution by downloading the source code.
2. Ensure the required libraries are installed (e.g., Polly and Dependency Injection packages).

---

### Integration Steps

1. **Inject the Modbus Service**

   Use `ModbusDependencyInjection.cs` to add the Modbus service to your application.

   Example implementation:

   ```csharp
   public static class ModBusService
   {
       public static void AddModbusService(this IServiceCollection services,
           IConfiguration configuration)
       {
           services.AddModbus();
           services.Configure<ModBusConnectionConfig>(configuration.GetSection("ModBusConnection"));
       }
   }
   ```

### Integration Steps

2. **Configure the PLC Connection**

   Define the connection settings in your `appsettings.json` file. Example configuration:

   ```json
   {
       "ModBusConnection": {
           "ConnectionType": "TCP", // Options: TCP, RTU
           "Ip": "10.20.30.50",
           "Port": 502,
           "COMName": "COM5",
           "BaudRate": 115200,
           "DataBits": 8,
           "StopBits": 1, // Options: None = 0, One = 1, Two = 2, OnePointFive = 3
           "Parity": 1 // Options: None = 0, Odd = 1, Even = 2, Mark = 3, Space = 4
       }
   }
   ```

  3. **Use the Service**

   After configuring, you can inject and use the Modbus service in your controllers or other application components. Example usage:

   ```csharp
   public class ExampleController : ControllerBase
   {
       private readonly IModbusCommands _modbus;

       public ModbusCommunication30G(IModbusCommands modbus)
        {
            _modbus = modbus;
        }

        [AllowAnonymous]
        [HttpPost("sendDInt")]
        public async Task SendValue(string address, int value) => await _modbus.SendInt(address, value);
   }
   ```
   
 ## Author  

Developed by **Lucas André Frizzi**.  

- [LinkedIn Profile](https://www.linkedin.com/in/lucas-frizzi/)  

Feel free to reach out for questions or collaboration opportunities!

---

## License  

This project is open-source. You can modify and use it according to your needs.

