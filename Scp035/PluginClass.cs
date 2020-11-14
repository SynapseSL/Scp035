using Synapse;
using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace Scp035
{
    [PluginInformation(
        Name = "Scp035",
        Author = "Dimenzio",
        Description = "Adds the Role Scp035 to the Game",
        LoadPriority = 1,
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.0.0"
        )]
    public class PluginClass : AbstractPlugin
    {
        private static Translation trans;

        [Synapse.Api.Plugin.Config(section = "Scp035")]
        public static Config Config;

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

            var translation = new Dictionary<string, string>
            {
                {"035pickup","This Item is an Scp035 Item, so if you drop it and a other Player takes it, Scp035 will take the player as his host" },
                {"survived035" , "This was Scp035 but you have survived it" }
            };
            Translation.CreateTranslations(translation);
            trans = Translation;

            new EventHandlers();
        }

        public static string GetTranslation(string key) => trans.GetTranslation(key);
    }
}
