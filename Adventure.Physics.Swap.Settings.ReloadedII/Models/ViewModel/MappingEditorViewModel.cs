using Adventure.Physics.Swap.Settings.ReloadedII.Models.Model;
using Adventure.Physics.Swap.Shared.Configs;

namespace Adventure.Physics.Swap.Settings.ReloadedII.Models.ViewModel;

public class MappingEditorViewModel<TSourceEnum, TTargetEnum> where TSourceEnum : notnull
{
    public KeyValuePairCollection<TSourceEnum, TTargetEnum> Mapping { get; set; }
    public Action<Dictionary<TSourceEnum, TTargetEnum>> ApplyToConfig { get; set; }
    public Config Config { get; set; }

    public MappingEditorViewModel(MappingEditorParameters<TSourceEnum, TTargetEnum> parameters, Config config)
    {
        Mapping       = new KeyValuePairCollection<TSourceEnum, TTargetEnum>(parameters.Mappings);
        ApplyToConfig = parameters.ApplyToConfig;
        Config        = config;
    }

    public void Save()
    {
        var dictionary = Mapping.AsDictionary();
        ApplyToConfig(dictionary);
        Config.ToJson(Environment.CurrentDirectory);
    }
}