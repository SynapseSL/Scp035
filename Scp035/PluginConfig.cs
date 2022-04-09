using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scp035
{
    public class PluginConfig : AbstractConfigSection
    {
        [Description("The Health of Scp035")]
        public int Scp035Health = 150;

        [Description("The text which gets displayed over the Player Name of Scp035")]
        public string DisplayName = "Scp-035";

        [Description("If the Spectator that are the longest time death should become 035")]
        public bool DeathTime = true;

        [Description("The max amount of Scp035 items that can exist")]
        public int Scp035ItemsAmount = 3;

        [Description("The Items that the Scp035 mask can be")]
        public List<int> Possible035Items = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 30, 31, 32, 33, 34 };

        [Description("The interval in which the mask should change its position")]
        public float PickupSpawnInterval = 30f;

        [Description("If Enabled Scp035 can Hurt Scps(Guns,Generator,FemurBreaker,etc.)")]
        public bool ff = false;
    }
}
