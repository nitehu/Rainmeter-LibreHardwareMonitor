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
using System.Runtime.InteropServices;
using Rainmeter;

namespace RainMeterLibreHardwareMonitor
{
    /// <summary>
    /// Rainmeter Plugin
    /// </summary>
    /// <remarks>
    /// This class was created from the RainMeter Plugin SDK. See https://github.com/rainmeter/rainmeter-plugin-sdk
    /// This class will be converted to a .dll that can be used by RainMeter. See https://github.com/3F/DllExport
    /// </remarks>
    public class Plugin
    {
        [DllExportAttribute]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
            API api = rm;
            Measure measure = data;
            measure.Initialize(api);
        }

        [DllExportAttribute]
        public static void Finalize(IntPtr data)
        {
            Measure measure = data;
            measure.Close();
            GCHandle.FromIntPtr(data).Free();
        }

        [DllExportAttribute]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = data;
        }

        [DllExportAttribute]
        public static double Update(IntPtr data)
        {
            Measure measure = data;

            return measure.Update();            
        }
    }
}