using Neuron.Core.Plugins;
using Ninject;
using PlayerRoles;
using Synapse3.SynapseModule;
using Synapse3.SynapseModule.Item;
using Synapse3.SynapseModule.Map;
using Synapse3.SynapseModule.Player;
using System.Linq;
using UnityEngine;

namespace Scp035;


[HeavyModded]
[Plugin(
Name = "Scp035",
Description = "Adds the Role Scp035 to the Game",
Version = "2.0.0",
Author = "Dimenzio"
)]
public class Scp035Plugin : ReloadablePlugin<PluginConfig, PluginTranslation>
{

    [Inject]
    public PlayerService PlayerService { get; set; }

    [Inject]
    public ItemService IteamService { get; set; }

    public override void Load()
    {
        var itemService = Synapse.Get<ItemService>();

        var allItemType = (ItemType[])System.Enum.GetValues(typeof(ItemType));
        foreach (var type in allItemType)
        {
            if (type == ItemType.None) continue;
            itemService.RegisterItem(new ItemAttribute()
            {
                BasedItemType = type,
                Id = (uint)type + 100,
                Name = $"Scp035-Item-{type}",
            });
        }
    }

    /// <summary>
    /// Spawn the SCP 035 items with random itemType on a other random item of an amout <see cref="PluginConfig.Scp035ItemsAmount"/>.
    /// Only if there are spectator
    /// </summary>
    public void SpawnScp035Items()
    {
        RemoveScp035Items();

        var spectators = PlayerService.GetPlayers(x => x.RoleID == (int)RoleTypeId.Spectator && !x.OverWatch);

        if (spectators.Count < 1)
            return;

        var Scp035Items = IteamService.AllItems.Where(x => x.IsScp035Item());
        
        for (int i = Config.Scp035ItemsAmount - Scp035Items.Count(); i > 0; i--)
        {
            if (Config.Possible035Items.Count < 1) return;

            var items = IteamService.AllItems.Where(x => x.State == ItemState.Map).ToList();
            if (items.Count == 0) return;
            
            var position = items[Random.Range(0, items.Count)].Position;
            
            Create035Item().Drop(position);
        }
    }

    /// <summary>
    /// Create a new SCP 035 item with a itemType included in <see cref="PluginConfig.Possible035Items"/>.
    /// </summary>
    public SynapseItem Create035Item()
    {
        var typeId = Config.Possible035Items[Random.Range(0, Config.Possible035Items.Count)];
        return new SynapseItem(typeId + 100);
    }

    /// <summary>
    /// Create a new SCP 035 of a specific itemType
    /// </summary>
    /// <param name="itemType">The disred itemType</param>
    public SynapseItem Create035Item(ItemType itemType)
    {
        if (itemType == ItemType.None) return null;

        return new SynapseItem(itemType + 100);
    }

    /// <summary>
    /// Remove the Scp035 present in the round.
    /// </summary>
    public void RemoveScp035Items(bool clearInventory = false)
    {
        if (clearInventory)
        {
            foreach (var item in IteamService.AllItems.Where(x => x.IsScp035Item()).ToArray())
                item.Destroy();
        }
        else
        {
            foreach (var item in IteamService.AllItems.Where(x => x.IsScp035Item() && x.State == ItemState.Map).ToArray())
                item.Destroy();
        }
    }
}

/*
            ScpPickup035 = "Du hast <color=red>SCP-035</color> aufgeboben. Gib es einem anderem Spieler damit er zu <color=red>SCP-035</color> wird",
            Survived035 = "Das war <color=red>SCP-035</color> aber du hast es überlebt",
            InteractWith035 = "Du kannst nicht mit einem <color=red>SCP-035</color> Item interagieren",
            Pickup035 = "<b>Du hast <color=red>SCP-035</color> aufgehoben</b>",
            Spawn035 = "<b>Du bist jetzt <color=red>SCP-035</color></b>",
            KilledBy035 = "<b>Du wurdest von <color=red>SCP-035</color> umgebracht</b>"
*/

