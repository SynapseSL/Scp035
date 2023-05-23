using MEC;
using Neuron.Core.Events;
using Neuron.Core.Meta;
using Ninject;
using PlayerRoles;
using Synapse3.SynapseModule;
using Synapse3.SynapseModule.Events;
using Synapse3.SynapseModule.Player;
using System.Collections.Generic;
using System.Linq;

namespace Scp035;

[Automatic]
public class EventHandlers : Listener
{
    [Inject]
    public Scp035Plugin Plugin { get; set; }
    
    [Inject]
    public PlayerService Player { get; set; }

    public CoroutineHandle SpawnIteam { get; private set; }

    [EventHandler]
    public void Restart(RoundStartEvent _) => Timing.KillCoroutines(SpawnIteam);

    [EventHandler]
    public void Start(RoundEndEvent _) => SpawnIteam = Timing.RunCoroutine(Respawn035Items());


    [EventHandler]
    public void OnDeath(DeathEvent ev)
    {
        if (ev.Player != ev.Attacker && ev.Attacker?.RoleID == 35)
            ev.Player.SendWindowMessage(Plugin.Translation.Get(ev.Player).KilledBy035);
    }

    [EventHandler]
    public void OnUseItem(BasicItemInteractEvent ev)
    {
        if (ev.Item.IsScp035Item())
        {
            ev.Allow = false;
            ev.Player.SendHint(Plugin.Translation.Get(ev.Player).InteractWith035);
        }
    }

    [EventHandler]
    public void Pickup(PickupEvent ev)
    {
        if (!ev.Item.IsScp035Item()) return;

        if (!Synapse3Extensions.CanHarmScp(ev.Player, false)
            || Plugin.Config.ImunisedRoleIds.Contains(ev.Player.RoleID))
        {
            ev.Player.SendBroadcast(ev.Player.GetTranslation(Plugin.Translation).ScpPickup035, 8);
        }
        else
        {
            ev.Allow = false;

            var possible035 = Player.GetPlayers(x => x.RoleID == (uint)RoleTypeId.Spectator && !x.OverWatch);

            if (possible035.Count == 0)
            {
                ev.Player.SendBroadcast(Plugin.Translation.Get(ev.Player).Survived035, 8);
                Plugin.RemoveScp035Items(true);
                return;
            }

            SynapsePlayer player;

            if (Plugin.Config.DeathTime)
            {
                possible035 = possible035.OrderBy(x => x.DeathTime).ToList();
                player = possible035.First();
            }
            else
            {
                player = possible035[UnityEngine.Random.Range(0, possible035.Count)];
            }

            var role = Synapse.Create<Scp035PlayerScript>(false);
            role.Target = ev.Player;
            player.CustomRole = new Scp035PlayerScript();
            Plugin.RemoveScp035Items(true);
        }
    }

#if DEBUG
    [EventHandler]
    public void OnKeyPress(KeyPressEvent ev)
    {
        switch (ev.KeyCode)
        {
            case UnityEngine.KeyCode.Alpha1:
                Plugin.SpawnScp035Items();
                break;

            case UnityEngine.KeyCode.Alpha2:
                Plugin.Create035Item().Drop(ev.Player.Position);
                break;

            case UnityEngine.KeyCode.Alpha3:
                ev.Player.RoleID = 35;
                break;
        }
    }
#endif

    private IEnumerator<float> Respawn035Items()
    {
        while (true)
        {
            if (!Player.Players.Any(x => x.RoleID == 35))
                Plugin.SpawnScp035Items();
            yield return Timing.WaitForSeconds(Plugin.Config.PickupSpawnInterval);
        }
    }
}
