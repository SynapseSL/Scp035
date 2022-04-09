using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;

namespace Scp035
{
    [PluginInformation(
        Name = "Scp035",
        Author = "Dimenzio",
        Description = "Adds the Role Scp035 to the Game",
        LoadPriority = 1,
        SynapseMajor = 2,
        SynapseMinor = 9,
        SynapsePatch = 0,
        Version = "v.1.1.3"
        )]
    public class PluginClass : AbstractPlugin
    {
        [Config(section = "Scp035")]
        public static PluginConfig Config;

        [SynapseTranslation]
        public static new SynapseTranslation<PluginTranslation> Translation { get; set; }

        public override void Load()
        {
            Server.Get.RoleManager.RegisterCustomRole<Scp035PlayerScript>();

            foreach(var type in (ItemType[])System.Enum.GetValues(typeof(ItemType)))
                if(type != ItemType.None)
                {
                    Server.Get.ItemManager.RegisterCustomItem(new Synapse.Api.Items.CustomItemInformation
                    {
                        BasedItemType = type,
                        ID = (int)type + 100,
                        Name = $"Scp035-Item-{type}"
                    });
                }

            Translation.AddTranslation(new PluginTranslation());
            Translation.AddTranslation(new PluginTranslation
            {
                ScpPickup035 = "Du hast <color=red>SCP-035</color> aufgeboben. Gib es einem anderem Spieler damit er zu <color=red>SCP-035</color> wird",
                Survived035 = "Das war <color=red>SCP-035</color> aber du hast es überlebt",
                InteractWith035 = "Du kannst nicht mit einem <color=red>SCP-035</color> Item interagieren",
                Pickup035 = "<b>Du hast <color=red>SCP-035</color> aufgehoben</b>",
                Spawn035 = "<b>Du bist jetzt <color=red>SCP-035</color></b>",
                KilledBy035 = "<b>Du wurdest von <color=red>SCP-035</color> umgebracht</b>"
            }, "GERMAN");

            new EventHandlers();
        }
    }
}
