using System.Reflection;

namespace MultiplayerCore.Helpers
{
    public class SongCoreConfig
    {
        private static object? sConfiguration = null;

        public static object? GetInstance()
        {
            sConfiguration ??= typeof(SongCore.Plugin)
                .GetProperty("Configuration", BindingFlags.NonPublic | BindingFlags.Static)
                ?.GetValue(null);
            
            if (sConfiguration is null)
                Plugin.Logger.Error("SongCore configuration instance not found - mod update may be needed");
            
            return sConfiguration;
        }

        public static object? GetValue(string key)
        {
            var configInstance = GetInstance();
            
            if (configInstance is null)
                return null;
            
            var configType = typeof(SongCore.Plugin).Assembly.GetType("SongCore.SConfiguration");
            var rfProp = configType.GetProperty(key);
            
            if (rfProp is null)
                Plugin.Logger.Error($"SongCore configuration key invalid: \"{key}\" - mod update may be needed");

            return rfProp?.GetValue(configInstance);
        }

        public static bool GetBool(string key)
        {
            var value = GetValue(key);
            
            if (value == null)
                return false;
            
            return (bool) value;
        }
    }
}