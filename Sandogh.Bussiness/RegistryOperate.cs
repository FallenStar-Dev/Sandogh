using Microsoft.Win32;
using System.Linq;

namespace Sandogh.Bussiness
{
    public static class RegistryOperator
    {
        private static void CreateEmptyConnectionKey(out RegistryKey regKey)
        {
            regKey = Registry.CurrentUser.CreateSubKey(@"software\Sandogh");
        }
        public static bool IsKeyExist(string key)
        {
            CreateEmptyConnectionKey(out var registryKey);
            return registryKey.GetValueNames().Contains(key).Equals(true);
        }
        public static void CreateKey(string key,string value)
        {
            CreateEmptyConnectionKey(out var registryKey);
            registryKey?.SetValue(key, value);
            registryKey?.Close();
            registryKey?.Dispose();
        }


        public static string GetKey(string key)
        {
            CreateEmptyConnectionKey(out var registryKey);
            if (IsKeyExist(key))
            {
                var connectionString = registryKey.GetValue(key).ToString();
                registryKey.Close();
                registryKey.Dispose();
                return connectionString;
            }

            return null;
        }
        
    }
}
