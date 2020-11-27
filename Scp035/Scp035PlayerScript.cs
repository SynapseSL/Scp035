using MEC;
using Synapse.Api;
using System.Collections.Generic;

namespace Scp035
{
    public class Scp035PlayerScript : Synapse.Api.Roles.Role
    {
        private Player _target;

        public Scp035PlayerScript() { }
        public Scp035PlayerScript(Player target) => _target = target;

        public override int GetRoleID() => 35;

        public override string GetRoleName() => "Scp035";

        public override List<Team> GetFriends()
        {
            return PluginClass.Config.ff ? new List<Team>() : new List<Team> { Team.SCP };
        }

        public override Team GetTeam() => Team.SCP;

        public override void Spawn()
        {
            if(_target == null)
                Player.RoleType = RoleType.ChaosInsurgency;
            else
            {
                Player.RoleType = _target.RoleType;

                Player.Inventory.Clear();
                foreach (var item in _target.Inventory.Items)
                {
                    item.Despawn();
                    Player.Inventory.Items.Add(item);
                }

                Timing.CallDelayed(0.2f, () => Player.Position = _target.Position);
                Player.Ammo5 = _target.Ammo5;
                Player.Ammo7 = _target.Ammo7;
                Player.Ammo9 = _target.Ammo9;

                _target.RoleID = (int)RoleType.Spectator;

                _target.SendBroadcast(5, PluginClass.GetTranslation("pickup035"));
            }

            Player.Health = PluginClass.Config.Scp035Health;
            Player.MaxHealth = PluginClass.Config.Scp035Health;
            Player.DisplayInfo = $"<color={PluginClass.Config.DisplayColor}>{PluginClass.Config.DisplayName}</color>";

            Player.SendBroadcast(5, PluginClass.GetTranslation("spawn035"));
        }

        public override void DeSpawn() => Player.DisplayInfo = "";
    }
}
