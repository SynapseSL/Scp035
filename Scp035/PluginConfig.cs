using Neuron.Core.Meta;
using Neuron.Core.Plugins;
using PlayerRoles;
using Syml;
using Synapse3.SynapseModule;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using YamlDotNet.Core.Tokens;

namespace Scp035;

[Automatic]
[DocumentSection("SCP035")]
public class PluginConfig : IDocumentSection
{
    [Description("The Health of Scp035")]
    public int Scp035Health { get; set; } = 150;

    [Description("The text which gets displayed over the Player Name of Scp035")]
    public string DisplayName { get; set; } = "Scp-035";

    [Description("If the Spectator that are the longest time death should become 035")]
    public bool DeathTime { get; set; } = true;

    [Description("The max amount of Scp035 items that can exist")]
    public int Scp035ItemsAmount { get; set; } = 3;

    [Description("The Items that the Scp035 mask can be")]
    public List<uint> Possible035Items { get; set; } = new List<uint> 
    { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 30, 31, 32, 33, 34, 35, 36, 37,
      38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51 };

    [Description("The interval in which the mask should change its position")]
    public float PickupSpawnInterval { get; set; } = 30f;

    [Description("If Enabled Scp035 can Hurt Scps(Guns,Generator,FemurBreaker,etc.)")]
    public bool ff { get; set; } = false;

    [Description("If Enabled Scp035 can Hurt Scps(Guns,Generator,FemurBreaker,etc.)")]
    public List<uint> ImunisedRoleIds { get; set; } = new List<uint>() { (uint)RoleTypeId.Tutorial };

}
