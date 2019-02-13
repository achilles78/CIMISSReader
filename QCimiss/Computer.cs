namespace QCimiss
{
    using System;
    using System.Management;

    public class Computer
    {
        private static Computer _instance;
        public string ComputerName;
        public string CpuID;
        public string DiskID;
        public string IpAddress;
        public string LoginUserName;
        public string MacAddress;
        public string SystemType;
        public string TotalPhysicalMemory;

        public Computer()
        {
            this.CpuID = this.GetCpuID();
            this.MacAddress = this.GetMacAddress();
            this.DiskID = this.GetDiskID();
            this.IpAddress = this.GetIPAddress();
            this.LoginUserName = this.GetUserName();
            this.SystemType = this.GetSystemType();
            this.TotalPhysicalMemory = this.GetTotalPhysicalMemory();
            this.ComputerName = this.GetComputerName();
        }

        public string GetComputerName()
        {
            try
            {
                return Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetCpuID()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2.Properties["ProcessorId"].Value.ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetDiskID()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_DiskDrive").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = (string) obj2.Properties["Model"].Value;
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetIPAddress()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    if ((bool) obj2["IPEnabled"])
                    {
                        Array array = (Array) obj2.Properties["IpAddress"].Value;
                        str = array.GetValue(0).ToString();
                        break;
                    }
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetMacAddress()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    if ((bool) obj2["IPEnabled"])
                    {
                        str = obj2["MacAddress"].ToString();
                        break;
                    }
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetSystemType()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["SystemType"].ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetTotalPhysicalMemory()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["TotalPhysicalMemory"].ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public string GetUserName()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["UserName"].ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public static Computer Instance()
        {
            if (_instance == null)
            {
                _instance = new Computer();
            }
            return _instance;
        }
    }
}

