using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using ETM.WCCOA;

namespace ConsoleApp2
{
    static class SystemParametrs
    {
        public static string driveName;
        public static Dictionary<string,dynamic> ParamPC()
        {
            Dictionary<string, dynamic> pairs = new Dictionary<string, dynamic>();
            pairs.Add("VersionWin", Environment.OSVersion.ToString());
            pairs.Add("CountCPU", Convert.ToDouble(Environment.ProcessorCount));
            pairs.Add("ArchitectureCPU", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString());
            pairs.Add("ModelCPU", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString());
            pairs.Add("LoadPercentageCPU", PercentCPU());
            pairs.Add("TemperatureCPU", TemperatureCPU());
            return pairs;
        }

        public static List<Dictionary<string, dynamic>> ParamHD()
        {
            ManagementObjectSearcher searcher =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_Volume WHERE (Drivetype=3 OR DriveType=2) AND DriveLetter IS NOT NULL AND Caption IS NOT NULL");

            List<Dictionary<string, dynamic>> list = new List<Dictionary<string, dynamic>>();

            foreach (ManagementObject queryObj in searcher.Get())
            {
                Dictionary<string, dynamic> pairs = new Dictionary<string, dynamic>();
                driveName = queryObj["Caption"].ToString().Replace(":\\","");
                pairs.Add($"Capacity_{driveName}", ConvertByteInGByte(queryObj["Capacity"]));
                pairs.Add($"Caption_{driveName}", driveName);
                pairs.Add($"FileSystem_{driveName}", queryObj["FileSystem"].ToString());
                pairs.Add($"FreeSpace_{driveName}", ConvertByteInGByte(queryObj["FreeSpace"]));
                list.Add(pairs);
            }
            return list;
        }

        public static Dictionary<string, dynamic> ParamRAM()
        {
            ManagementObjectSearcher ramMonitor = 
            new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

            Dictionary<string, dynamic> pairs = new Dictionary<string, dynamic>();

            foreach (ManagementObject objram in ramMonitor.Get())
            {
                double totalRam = Math.Round(Convert.ToDouble((objram["TotalVisibleMemorySize"])) / 1024 / 1024, 2);
                double busyRam = Math.Round(Convert.ToDouble((objram["FreePhysicalMemory"]))/1024/1024, 2);
                double percentRam = Math.Round(busyRam * 100 / totalRam,2);

                pairs.Add("TotalMemorySize", totalRam);
                pairs.Add("FreePhysicalMemory", busyRam);
                pairs.Add("PercentFreeMemory", percentRam);
            }
            return pairs;
        }
        

        static int PercentCPU()
        {
            int i = 0;
            ManagementObjectSearcher namager = new ManagementObjectSearcher("SELECT LoadPercentage  FROM Win32_Processor");
            foreach (ManagementObject obj in namager.Get())
            {
                i= Convert.ToInt32(obj["LoadPercentage"]);
            }
            return i;
        }

        public static string MachineName()
        {
            return Environment.MachineName;
        }

        static double TemperatureCPU()
        {
            try
            {
                double d = 0;
                ManagementObjectSearcher namager = new ManagementObjectSearcher("root\\WMI","SELECT * FROM MSAcpi_ThermalZoneTemperature");
                foreach (ManagementObject obj in namager.Get())
                {
                    d= Math.Round(Convert.ToDouble(Convert.ToDouble(obj.GetPropertyValue("CurrentTemperature".ToString())) - 2732) / 10.0, 2);
                }
                return d;
            }
            catch
            {
                return -1;
            }
        }

        static double ConvertByteInGByte(object obj)
        {
            return Math.Round(Convert.ToDouble(obj) / 1024 / 1024 / 1024, 2);
        }
    }
}
