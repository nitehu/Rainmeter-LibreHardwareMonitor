// Copyright (C) 2021 Rainmeter-LibreHardwareMonitor Developers
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using LibreHardwareMonitor.Hardware;

namespace ListSensors
{
    /// <summary>
    /// Console App to list the sensors detected by LibreHardwareMonitor
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please wait, enumerating Sensors");

            Computer computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };

            computer.Open();
            computer.Accept(new HardwareInfoUpdateVisitor());

            foreach (IHardware hardware in computer.Hardware)
            {
                Console.WriteLine($"Hardware: {hardware.Name}; Id: {hardware.Identifier}");

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    Console.WriteLine($"    Subhardware: {subhardware.Name}; Id: {subhardware.Identifier}");

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        if (sensor.Value == null) continue;
                        Console.WriteLine($"        Sensor: {sensor.Name}; Id: {sensor.Identifier}; Value: {sensor.Value} {UnitFromSensorType(sensor.SensorType)}");
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    if (sensor.Value == null) continue;
                    Console.WriteLine($"  Sensor: {sensor.Name}; Id: {sensor.Identifier}; Value: {sensor.Value} {UnitFromSensorType(sensor.SensorType)}");
                }
            }

            computer.Close();

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        private static string UnitFromSensorType(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage:
                    return "V";
                case SensorType.Clock:
                    return "Hz";
                case SensorType.Temperature:
                    return "C";
                case SensorType.Load:
                    return "%";
                case SensorType.Frequency:
                    return "MHz";
                case SensorType.Fan:
                    return "RPM";
                case SensorType.Flow:
                    return "";
                case SensorType.Control:
                    return "%";
                case SensorType.Level:
                    return "";
                case SensorType.Factor:
                    return "";
                case SensorType.Power:
                    return "W";
                case SensorType.Data:
                    return "GB";
                case SensorType.SmallData:
                    return "";
                case SensorType.Throughput:
                    return "KB/s";
                default: return "";
            }
        }
    }
}
