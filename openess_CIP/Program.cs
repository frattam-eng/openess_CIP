using Siemens.Engineering;
using Siemens.Engineering.Cax;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.SW;
using System;
using System.IO;

namespace openess_CIP
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectPath =
                @"C:\Users\Siemens\Documents\Automation\CIP\CIP V1.2_V18\CIP V1.2_V18.ap18";

            string exportFolder = @"C:\tmp";

            try
            {
                Directory.CreateDirectory(exportFolder);

                using (var tiaPortal =
                    new TiaPortal(TiaPortalMode.WithUserInterface))
                {
                    Console.WriteLine("Opening project...");

                    Project project = tiaPortal.Projects.Open(
                        new FileInfo(projectPath));

                    // Get the CAx export/import service FROM THE PROJECT.
                    CaxProvider caxProvider =
                        project.GetService<CaxProvider>();

                    if (caxProvider == null)
                    {
                        throw new Exception(
                            "CaxProvider is unavailable for this project.");
                    }

                    FileInfo amlFile = new FileInfo(
                        Path.Combine(
                            exportFolder,
                            "CIP_cs_HardwareNetwork.aml"));

                    FileInfo logFile = new FileInfo(
                        Path.Combine(
                            exportFolder,
                            "CIP_cs_HardwareNetwork.log"));

                    Console.WriteLine(
                        "Exporting hardware and network data...");

                    // Export all project CAx data: devices, modules,
                    // networks, IO systems, configured interfaces, etc.
                    caxProvider.Export(project, amlFile, logFile);

                    Console.WriteLine("CAx export finished.");
                    Console.WriteLine("AML: " + amlFile.FullName);
                    Console.WriteLine("Log: " + logFile.FullName);

                    Console.WriteLine("Press any key to close.");
                    Console.ReadKey();

                    project.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CAx export failed:");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}