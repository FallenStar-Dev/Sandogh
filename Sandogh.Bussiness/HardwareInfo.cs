using System;
using System.Management;

namespace Sandogh.Bussiness
{
    public static class HardwareInfo
    {
        /// <summary>
        /// Retrieving Processor Id.
        /// </summary>
        /// <returns></returns>
        /// 

        public static string GetProcessorId()
        {

            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            var id = string.Empty;
            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;

                id = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return id;

        }
        /// <summary>
        /// Retrieving HDD Serial No.
        /// </summary>
        /// <returns></returns>
        public static string GetHddSerialNo()
        {
            using var mgt = new ManagementClass("Win32_LogicalDisk");
            using var mcol = mgt.GetInstances();
            string result = "";
            foreach (var o in mcol)
            {
                var strt = (ManagementObject)o;
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            mcol.Dispose();
            mgt.Dispose();
            return result;
        }
        /// <summary>
        /// Retrieving System MAC Address.
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            var macAddress = string.Empty;
            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                if (macAddress == string.Empty)
                {
                    if ((bool)mo["IPEnabled"]) macAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }

            macAddress = macAddress.Replace(":", "");
            return macAddress;
        }
        /// <summary>
        /// Retrieving Motherboard Manufacturer.
        /// </summary>
        /// <returns></returns>
        public static string GetBoardMaker()
        {

            using var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }

                catch
                {
                    // ignored
                }
            }
            searcher.Dispose();
            return "Board Maker: Unknown";

        }
        /// <summary>
        /// Retrieving Motherboard Product Id.
        /// </summary>
        /// <returns></returns>
        public static string GetBoardProductId()
        {
            using var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("Product").ToString();
                }
                catch
                {
                    // ignored
                }
            }
            searcher.Dispose();
            return "Product: Unknown";

        }
        /// <summary>
        /// Retrieving CD-DVD Drive Path.
        /// </summary>
        /// <returns></returns>
        public static string GetCdRomDrive()
        {

            using var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive");

            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("Drive").ToString();
                }
                catch
                {
                    // ignored
                }
            }
            searcher.Dispose();
            return "CD ROM Drive Letter: Unknown";

        }
        /// <summary>
        /// Retrieving BIOS Maker.
        /// </summary>
        /// <returns></returns>
        public static string GetBiosMaker()
        {
            using var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch
                {
                    // ignored
                }
            }
            searcher.Dispose();
            return "BIOS Maker: Unknown";

        }
        /// <summary>
        /// Retrieving BIOS Serial No.
        /// </summary>
        /// <returns></returns>
        public static string GetBiosSerialNumber()
        {
            var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("SerialNumber").ToString();
                }
                catch
                {
                    // ignored
                }
            }

            return "BIOS Serial Number: Unknown";

        }
        /// <summary>
        /// Retrieving BIOS Caption.
        /// </summary>
        /// <returns></returns>
        public static string GetBiosCaption()
        {
            var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("Caption").ToString();
                }
                catch
                {
                    // ignored
                }
            }
            return "BIOS Caption: Unknown";
        }
        /// <summary>
        /// Retrieving System Account Name.
        /// </summary>
        /// <returns></returns>
        public static string GetAccountName()
        {
            var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_UserAccount");
            foreach (var o in searcher.Get())
            {
                var wmi = (ManagementObject)o;
                try
                {
                    return wmi.GetPropertyValue("Name").ToString();
                }
                catch
                {
                    // ignored
                }
            }
            return "User Account Name: Unknown";

        }
        /// <summary>
        /// Retrieving Physical Ram Memory.
        /// </summary>
        /// <returns></returns>
        public static string GetPhysicalMemory()
        {
            var oMs = new ManagementScope();
            var oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            var oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            var oCollection = oSearcher.Get();
            long memSize = 0;

            // In case more than one Memory sticks are installed
            foreach (var o in oCollection)
            {
                var obj = (ManagementObject)o;
                var mCap = Convert.ToInt64(obj["Capacity"]);
                memSize += mCap;
            }
            memSize = (memSize / 1024) / 1024;
            return memSize.ToString() + "MB";
        }
        /// <summary>
        /// Retrieving No of Ram Slot on Motherboard.
        /// </summary>
        /// <returns></returns>
        public static string GetNoRamSlots()
        {

            var memSlots = 0;
            var oMs = new ManagementScope();
            var oQuery2 = new ObjectQuery("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
            var oSearcher2 = new ManagementObjectSearcher(oMs, oQuery2);
            var oCollection2 = oSearcher2.Get();
            foreach (var o in oCollection2)
            {
                var obj = (ManagementObject)o;
                memSlots = Convert.ToInt32(obj["MemoryDevices"]);
            }
            return memSlots.ToString();
        }
        //Get CPU Temperature.
        /// <summary>
        /// method for retrieving the CPU Manufacturer
        /// using the WMI class
        /// </summary>
        /// <returns>CPU Manufacturer</returns>
        public static string GetCpuManufacturer()
        {
            var cpuMan = string.Empty;
            //create an instance of the Management class with the
            //Win32_Processor class
            using var mgt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            using var objCol = mgt.GetInstances();
            //start our loop for all processors found
            foreach (var o in objCol)
            {
                using var obj = o as ManagementObject;
                if (cpuMan == string.Empty)
                {
                    // only return manufacturer from first CPU
                    cpuMan = obj?.Properties["Manufacturer"].Value.ToString();
                }
            }
            objCol.Dispose();
            mgt.Dispose();

            return cpuMan;
        }

        /// <summary>
        /// method to retrieve the CPU's current
        /// clock speed using the WMI class
        /// </summary>
        /// <returns>Clock speed</returns>
        public static int GetCpuCurrentClockSpeed()
        {
            var cpuClockSpeed = 0;
            //create an instance of the Management class with the
            //Win32_Processor class
            using var mgt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            using var objCol = mgt.GetInstances();
            //start our loop for all processors found
            foreach (var o in objCol)
            {
                var obj = (ManagementObject)o;
                if (cpuClockSpeed == 0)
                {
                    // only return cpuStatus from first CPU
                    cpuClockSpeed = Convert.ToInt32(obj.Properties["CurrentClockSpeed"].Value.ToString());
                }
            }
            objCol.Dispose();
            mgt.Dispose();
            //return the status
            return cpuClockSpeed;
        }
        /// <summary>
        /// method to retrieve the network adapters
        /// default IP gateway using WMI
        /// </summary>
        /// <returns>adapters default IP gateway</returns>
        public static string GetDefaultIpGateway()
        {
            //create out management class object using the
            //Win32_NetworkAdapterConfiguration class to get the attributes
            //of the network adapter
            using var mgt = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //create our ManagementObjectCollection to get the attributes with
            var objCol = mgt.GetInstances();
            var gateway = string.Empty;
            //loop through all the objects we find
            foreach (var o in objCol)
            {
                var obj = (ManagementObject)o;
                if (gateway == string.Empty)  // only return MAC Address from first card
                {
                    //grab the value from the first network adapter we find
                    //you can change the string to an array and get all
                    //network adapters found as well
                    //check to see if the adapter's IPEnabled
                    //equals true
                    if ((bool)obj["IPEnabled"])
                    {
                        gateway = obj["DefaultIPGateway"].ToString();
                    }
                }
                //dispose of our object
                mgt.Dispose();
                obj.Dispose();
            }
            //replace the ":" with an empty space, this could also
            //be removed if you wish
            gateway = gateway.Replace(":", "");
            //return the mac address
            return gateway;
        }
        /// <summary>
        /// Retrieve CPU Speed.
        /// </summary>
        /// <returns></returns>

        public static double? GetCpuSpeedInGHz()
        {
            double? ghz = null;
            using var mc = new ManagementClass("Win32_Processor");
            foreach (var o in mc.GetInstances())
            {
                var mo = (ManagementObject)o;
                ghz = 0.001 * (uint)mo.Properties["CurrentClockSpeed"].Value;
                break;
            }
            mc.Dispose();
            return ghz;
        }
    }




}
