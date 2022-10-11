using System.Text.Json;
using System.Text.Json.Serialization;
using Ninject;

namespace Adventure.Physics.Swap.Shared.Configs.Json;

public abstract class ObservableJsonSerializable<TType> : ObservableObject where TType : IInitializable, new() 
{
    private static JsonSerializerOptions _options = new()
    {
        Converters = { new JsonStringEnumConverter() },
        WriteIndented = true
    };

    public static TType FromPath(string filePath)
    {
        TType value;
        if (File.Exists(filePath))
        {
            string jsonFile = File.ReadAllText(filePath);
            value = JsonSerializer.Deserialize<TType>(jsonFile, _options)!;
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
        string directoryOfPath = Path.GetDirectoryName(fullPath)!;
        if (!Directory.Exists(directoryOfPath))
            Directory.CreateDirectory(directoryOfPath);

        string jsonFile = JsonSerializer.Serialize(config, _options);
        File.WriteAllText(fullPath, jsonFile);
    }
}