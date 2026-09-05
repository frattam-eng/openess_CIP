using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.SW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openess_CIP
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectPath = @"C:\Users\Siemens\Documents\Automation\CIP\CIP V1.2_V18\CIP V1.2_V18.ap18";
            string CICIP_csPFolder = @"C:\tmp";

            using (var tiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface)) 
            {
                var project = tiaPortal.Projects.Open(new FileInfo(projectPath));
                PlcSoftware plcSoftware = FindPlcSoftware(project);

                if (plcSoftware == null) 
                {
                    Console.WriteLine("No PLC Software Found");
                    return;
                }
                                 
                
            }

        }
        private static PlcSoftware FindPlcSoftware(Project project) 
        {
            foreach (Device device in project.Devices) 
            {
                foreach (DeviceItem deviceItem in device.DeviceItems) 
                {
                    var softwareContainer = deviceItem.GetService<SoftwareContainer>();
                    if (softwareContainer?.Software is PlcSoftware plcSoftware)
                    {
                        return plcSoftware;
                    }
                }
            }
            return null;
        }
    }


}


