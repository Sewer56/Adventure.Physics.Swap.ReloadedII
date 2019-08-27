using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ninject;
using Reloaded.WPF.MVVM;

namespace Adventure.Physics.Swap.Shared.Configs.Json
{
    public abstract class ObservableJsonSerializable<TType> : ObservableObject where TType : IInitializable, new() 
    {
        protected static JsonSerializerSettings Options = new JsonSerializerSettings()
        {
            Converters = { new StringEnumConverter() },
            Formatting = Formatting.Indented
        };

        public static TType FromPath(string filePath)
        {
            TType value;
            if (File.Exists(filePath))
            {
                string jsonFile = File.ReadAllText(filePath);
                value = JsonConvert.DeserializeObject<TType>(jsonFile, Options);
            }
            else
            {
                var newFile = new TType();
                ToPath(newFile, filePath);
                value = newFile;
            }

            value.Initialize();
            return value;
        }

        public static void ToPath(TType config, string filePath)
        {
            string fullPath = Path.GetFullPath(filePath);
            string directoryOfPath = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryOfPath))
                Directory.CreateDirectory(directoryOfPath);

            string jsonFile = JsonConvert.SerializeObject(config, Options);
            File.WriteAllText(fullPath, jsonFile);
        }
    }
}
