using Neuron.Core.Meta;
using Neuron.Core.Plugins;
using Ninject;
using PlayerRoles;
using Synapse3.SynapseModule;
using Synapse3.SynapseModule.Enums;
using Synapse3.SynapseModule.Player;
using Synapse3.SynapseModule.Role;
using System.Collections.Generic;

namespace Scp035;

[Automatic]
[Role(
    Name = "Scp035",
    Id = 35,
    TeamId = (uint)Team.SCPs
)]
public class Scp035PlayerScript : SynapseRole
{

    [Inject]
    public Scp035Plugin Plugin { get; set; }

    public SynapsePlayer Target { get; set; } = null;

    public CustomInfoList.CustomInfoEntry NameEntry { get; private set; }

    public CustomInfoList.CustomInfoEntry RoleEntry { get; private set; }

    public Scp035PlayerScript()
    {
        Plugin = Synapse.Get<Scp035Plugin>();
    }

    public override List<uint> GetFriendsID() => Plugin.Config.ff ? new List<uint>() : new List<uint> { (uint)Team.SCPs };

    public override void SpawnPlayer(ISynapseRole previousRole = null, bool spawnLite = false)
    {
        if (spawnLite)
        {
            SetDisplay();
            return;
        }

        if (Target == null)
        {
            Player.RoleType = RoleTypeId.ChaosRifleman;
        }
        else
        {
            Player.Position = Target.Position;
            Player.ChangeRoleLite(Target.RoleType);
            Player.Inventory.Clear();
            foreach (var item in Target.Inventory.Items)
            {
                item.Destroy();
                item.EquipItem(Player, provideFully: true);
            }

            foreach (var ammoType in (AmmoType[])System.Enum.GetValues(typeof(AmmoType)))
	        {
                Player.Inventory.AmmoBox[ammoType] = Target.Inventory.AmmoBox[ammoType];
            }  
        }

        Player.Health = Plugin.Config.Scp035Health;
        Player.MaxHealth = Plugin.Config.Scp035Health;

        SetDisplay();
    }

    public override void DeSpawn(DeSpawnReason reason)
    {
        ClearDisplay();
    }

    public void SetDisplay()
    {
        NameEntry = new CustomInfoList.CustomInfoEntry
        {
            Info = base.Player.NicknameSync.HasCustomName ? (base.Player.NicknameSync._displayName + "*") : base.Player.NickName
        };
        RoleEntry = new CustomInfoList.CustomInfoEntry
        {
            Info = Plugin.Config.DisplayName
        };

        Player.RemoveDisplayInfo(PlayerInfoArea.Nickname);
        Player.RemoveDisplayInfo(PlayerInfoArea.Role);
        Player.RemoveDisplayInfo(PlayerInfoArea.UnitName);
        Player.RemoveDisplayInfo(PlayerInfoArea.PowerStatus);
        Player.CustomInfo.Add(NameEntry);
        Player.CustomInfo.Add(RoleEntry);
    }

    public void ClearDisplay()
    {
        Player.AddDisplayInfo(PlayerInfoArea.Nickname);
        Player.AddDisplayInfo(PlayerInfoArea.Role);
        Player.AddDisplayInfo(PlayerInfoArea.UnitName);
        Player.AddDisplayInfo(PlayerInfoArea.PowerStatus);
        Player.CustomInfo.Remove(NameEntry);
        Player.CustomInfo.Remove(RoleEntry);
    }
}
