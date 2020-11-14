﻿using Synapse;
using Synapse.Api;
using Synapse.Api.Items;
using System.Linq;
using System.Collections.Generic;
using MEC;

namespace Scp035
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerPickUpItemEvent += Pickup;
            Server.Get.Events.Player.PlayerItemUseEvent += Use;
            Server.Get.Events.Player.PlayerShootEvent += Shoot;
            Server.Get.Events.Round.RoundStartEvent += Start;
            Server.Get.Events.Round.RoundRestartEvent += Restart;
        }

        private void Shoot(Synapse.Api.Events.SynapseEventArguments.PlayerShootEventArgs ev)
        {
            if (IsScp035Item(ev.Weapon))
                ev.Allow = false;
        }

        private void Use(Synapse.Api.Events.SynapseEventArguments.PlayerItemInteractEventArgs ev)
        {
            if (IsScp035Item(ev.CurrentItem))
                ev.Allow = false;
        }

        private CoroutineHandle _respawn;

        private void Restart() => Timing.KillCoroutines(_respawn);

        private void Start() => _respawn = Timing.RunCoroutine(Respawn035Items());

        private IEnumerator<float> Respawn035Items()
        {
            for(; ; )
            {
                if (!Server.Get.Players.Any(x => x.RoleID == 35))
                    Spawn035Item();
                yield return Timing.WaitForSeconds(PluginClass.Config.PickupSpawnInterval);
            }
        }

        private bool IsScp035Item(SynapseItem item) => item.Name.Contains("Scp035-Item-");

        public void Spawn035Item()
        {
            RemoveScp035Items();

            if (Server.Get.GetPlayers(x => x.RoleID == (int)RoleType.Spectator && !x.OverWatch).Count < 1)
                return;

            for (int i = PluginClass.Config.Scp035ItemsAmount - Map.Get.Items.Where(x => IsScp035Item(x)).Count(); i > 0; i--)
            {
                if (PluginClass.Config.Possible035Items.Count < 1) return;

                var type = PluginClass.Config.Possible035Items.ElementAt(UnityEngine.Random.Range(0, PluginClass.Config.Possible035Items.Count));
                var items = Map.Get.Items.Where(x => x.ItemHolder == null);
                var pos = items.ElementAt(UnityEngine.Random.Range(0, items.Count())).Position;

                var item = new SynapseItem(type + 100, 0f, 0, 0, 0);
                item.Drop(pos);
            }
        }

        private void RemoveScp035Items(bool clearinventory = false)
        {
            if (clearinventory)
                foreach (var item in Map.Get.Items.Where(x => IsScp035Item(x)).ToArray())
                    item.Destroy();
            else
                foreach (var item in Map.Get.Items.Where(x => IsScp035Item(x) && x.ItemHolder == null).ToArray())
                    item.Destroy();
        }

        private void Pickup(Synapse.Api.Events.SynapseEventArguments.PlayerPickUpItemEventArgs ev)
        {
            if (IsScp035Item(ev.Item))
            {
                if(ev.Player.RealTeam == Team.SCP || ev.Player.RoleID == (int)RoleType.Tutorial)
                    ev.Player.SendBroadcast(8, PluginClass.GetTranslation("035pickup"));
                else
                {
                    ev.Allow = false;

                    var players = Server.Get.GetPlayers(x => x.RoleID == (int)RoleType.Spectator && !x.OverWatch);

                    if(players.Count == 0)
                    {
                        ev.Player.SendBroadcast(8, PluginClass.GetTranslation("survived035"));
                        RemoveScp035Items(true);
                        return;
                    }

                    players.OrderBy(x => x.DeathTime);

                    Player player;

                    if (PluginClass.Config.DeathTime)
                        player = players.FirstOrDefault();
                    else
                        player = players.ElementAt(UnityEngine.Random.Range(0, players.Count));

                    player.CustomRole = new Scp035PlayerScript(ev.Player);
                    ev.Item.Destroy();
                }
            }
        }
    }
}
