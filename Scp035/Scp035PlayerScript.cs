using Synapse.Api;
using Synapse.Api.Enum;
using System.Collections.Generic;

namespace Scp035
{
    public class Scp035PlayerScript : Synapse.Api.Roles.Role
    {
        internal readonly Player _target;

        public Scp035PlayerScript() { }
        public Scp035PlayerScript(Player target) => _target = target;

        public override int GetRoleID() => 35;

        public override string GetRoleName() => "Scp035";

        public override List<int> GetFriendsID() => PluginClass.Config.ff ? new List<int>() : new List<int> { (int)Team.SCP };

        public override int GetTeamID() => (int)Team.SCP;

        public override void Spawn()
        {
            if(_target == null)
                Player.RoleType = RoleType.ChaosRifleman;
            else
            {
                Player.RoleType = _target.RoleType;

                Player.Inventory.Clear();
                foreach (var item in _target.Inventory.Items)
                {
                    item.Despawn();
                    item.PickUp(Player);
                }

                foreach (var type in (AmmoType[])System.Enum.GetValues(typeof(AmmoType)))
                    Player.AmmoBox[type] = _target.AmmoBox[type];

                _target.RoleID = (int)RoleType.Spectator;

                _target.SendBroadcast(5, PluginClass.Translation.ActiveTranslation.Pickup035);
            }

            Player.Health = PluginClass.Config.Scp035Health;
            Player.MaxHealth = PluginClass.Config.Scp035Health;
            Player.DisplayInfo = PluginClass.Config.DisplayName;

            Player.SendBroadcast(5, PluginClass.Translation.ActiveTranslation.Spawn035);
        }

        public override void DeSpawn()
        {
            Player.DisplayInfo = "";
            Map.Get.AnnounceScpDeath("0 3 5");
        }
    }
}
