# RainMeter-LibreHardwareMonitor Plugin

A [RainMeter](https://github.com/rainmeter/rainmeter) Plugin for [LibreHardwareMonitor](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor).

## Installation

Please be advised that this plugin has no installer wizard at the moment (and maybe never will), its installation and configuration require you to copy and edit files manually.  
  
1. Download and extract a [Release Package](https://github.com/nitehu/Rainmeter-LibreHardwareMonitor/releases)  
2. Stop Rainmeter if it is running
3. Copy the correct version (x86 or x64) of the Plugin from the `Plugin` folder to the RainMeter Plugin folder  
4. Copy the additional LibreHardwareInfo packages from the `Packages` folder to the RainMeter installation folder (usually in `c:\Program Files\Rainmeter`)
5. Use the ListSensors utility in the `ListSensors` folder to find the LibreHardwareMonitor Identifier of hardwares and sensors.
6. Restart Rainmeter.

LibreHardwareMonitor will work and show some sensors without starting Rainmeter with Administrator privileges. However, there are some Sensors which can only be read when Rainmeter is started as Administrator.  
To start Rainmeter as Administrator automatically, you can use for example the Windows Task Scheduler.  
  
The ListSensors utility will list the sensors available when it is started.  
ListSensors must be run as Administrator to list the sensors available only to Administrators.
  
## Configuration
  
To use a Measure provided by the Rainmeter-LibreHardwareMonitor Plugin in a Rainmeter Skin, you must set it up correctly in the Skin's .ini file.  
The plugin accepts the following parameters:  
- **HardwareIdentifier**: The identifier of the Hardware which contains the Sensor, as printed by ListSensors  
- **SubhardwareIdentifier**: The identifier of the Subhardware which contains the Sensor (if any), as printed by ListSensors. This setting is optional  
- **SensorIdentifier**: The identifier of the Sensor which contains the value for this Measure, as printed by ListSensors  

A sample Measure setup in the Skin's .ini file may look like the following:  

```
[LibreCPU0Temp]
Measure=Plugin
Plugin=RainMeterLibreHardwareMonitor.dll
HardwareIdentifier=/motherboard
SubhardwareIdentifier=/lpc/it8686e
SensorIdentifier=/lpc/it8686e/temperature/2
MinValue=10
MaxValue=90
```

## License

This software is licensed under the terms of the GNU General Public License version 3 (GPLv3).

Full text of the license is available in the [LICENSE](LICENSE) file and [online](https://opensource.org/licenses/gpl-3.0.html).
