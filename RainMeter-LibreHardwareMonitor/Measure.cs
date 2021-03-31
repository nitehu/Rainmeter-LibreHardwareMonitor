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
using System.Linq;
using System.Runtime.InteropServices;
using LibreHardwareMonitor.Hardware;
using Rainmeter;

namespace RainMeterLibreHardwareMonitor
{
    /// <summary>
    /// RainMeter Measure which returns the value of a LibreHardwareMonitor Sensor
    /// </summary>
    /// <remarks>
    /// This Measure reads the following settings from the RainMeter SKin .ini file:
    /// - HardwareIdentifier: The identifier of the Hardware which contains the Sensor, as printed by ListSensors
    /// - SubhardwareIdentifier: The identifier of the Subhardware which contains the Sensor (if any), as printed by ListSensors. This setting is optional
    /// - SensorIdentifier: The identifier of the Sensor which contains the value for this Measure, as printed by ListSensors
    /// </remarks>
    internal class Measure
    {
        private const string _hardwareIdentifierSettingString = "HardwareIdentifier";
        private const string _subhardwareIdentifierSettingString = "SubhardwareIdentifier";
        private const string _sensorIdentifierSettingString = "SensorIdentifier";

        private API _api;
        private Computer _computer;
        private IHardware _hardware;
        private ISensor _sensor;

        static public implicit operator Measure(IntPtr data)
        {
            return (Measure)GCHandle.FromIntPtr(data).Target;
        }

        internal void Initialize(API api)
        {
            try
            {
                _api = api;
                _computer = new Computer
                {
                    IsCpuEnabled = true,
                    IsGpuEnabled = true,
                    IsMemoryEnabled = true,
                    IsMotherboardEnabled = true,
                    IsControllerEnabled = true,
                    IsNetworkEnabled = true,
                    IsStorageEnabled = true
                };

                _computer.Open();

                string hardwareIdentifier = _api.ReadString(_hardwareIdentifierSettingString, "").Trim();
                _hardware = _computer.Hardware.SingleOrDefault(x => x.Identifier.ToString() == hardwareIdentifier);

                if (_hardware == null)
                {
                    _api.Log(API.LogType.Error, $"RainMeterLibreHardwareMonitor Measure didn't find Hardware with Identifier \"{hardwareIdentifier}\"");
                    return;
                }

                string subhardwareIdentifier = _api.ReadString(_subhardwareIdentifierSettingString, "").Trim();
                if (!string.IsNullOrEmpty(subhardwareIdentifier))
                {
                    _hardware.Update();
                    _hardware = _hardware.SubHardware.SingleOrDefault(x => x.Identifier.ToString() == subhardwareIdentifier);

                    if (_hardware == null)
                    {
                        _api.Log(API.LogType.Error, $"RainMeterLibreHardwareMonitor Measure didn't find Subhardware with Identifier \"{subhardwareIdentifier}\"");
                        return;
                    }
                }

                _hardware.Update();
                string sensorIdentifier = _api.ReadString(_sensorIdentifierSettingString, "").Trim();
                _sensor = _hardware.Sensors.SingleOrDefault(x => x.Identifier.ToString() == sensorIdentifier);

                if (_sensor == null)
                {
                    _api.Log(API.LogType.Error, $"RainMeterLibreHardwareMonitor Measure didn't find Sensor with Identifier \"{sensorIdentifier}\"");
                    return;
                }

                _api.Log(API.LogType.Notice, "RainMeterLibreHardwareMonitor Measure initialized");
            }
            catch (Exception exception)
            {
                _api.Log(API.LogType.Error, $"Exception while initializing RainMeterLibreHardwareMonitor Measure: {exception}");
            }

        }

        internal void Close()
        {
            _computer?.Close();
            _api.Log(API.LogType.Notice, "RainMeterLibreHardwareMonitor Measure closed");
        }

        internal double Update()
        {
            _hardware?.Update();
            return _sensor?.Value ?? 0;
        }
    }
}

