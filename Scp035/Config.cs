using Synapse.Config;
using System.Collections.Generic;

namespace Scp035
{
    public class Config : AbstractConfigSection
    {
        public int Scp035Health = 150;

        public string BadgeName = "Scp-035";

        public string BadgeColor = "red";

        public bool DeathTime = true;

        public int Scp035ItemsAmount = 3;

        public List<int> Possible035Items = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 30, 31, 32, 33, 34 };

        public float PickupSpawnInterval = 30f;
    }
}
