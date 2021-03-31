using LibreHardwareMonitor.Hardware;

namespace ListSensors
{
    /// <summary>
    /// Visitor to update all hardware detected by LibreHardwareMonitor
    /// </summary>
    /// <remarks>
    /// This class is from a sample by LibreHardwareMonitor.
    /// Please see https://github.com/LibreHardwareMonitor/LibreHardwareMonitor
    /// </remarks>
    public class HardwareInfoUpdateVisitor : IVisitor
    {

        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
