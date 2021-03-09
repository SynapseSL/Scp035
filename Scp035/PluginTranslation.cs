using Synapse.Translation;

namespace Scp035
{
    public class PluginTranslation : IPluginTranslation
    {
        public string ScpPickup035 { get; set; } = "This Item is an Scp035 Item, so if you drop it and a other Player takes it, Scp035 will take the player as his host";

        public string Survived035 { get; set; } = "This was Scp035 but you have survived it";

        public string InteractWith035 { get; set; } = "You can't use a Scp-035 Item for any interaction with it!";

        public string Pickup035 { get; set; } = "<b>You have picked up <color=red>Scp-035</color>.</b>";

        public string Spawn035 { get; set; } = "<b>You are <color=red>SCP-035</color></b>";

        public string KilledBy035 { get; set; } = "<b>You was killed by <color=red>SCP</color>-035</b>";
    }
}
