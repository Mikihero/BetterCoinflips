using Exiled.API.Features;
using System;
using Exiled.API.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;

using Player = Exiled.Events.Handlers.Player;


namespace BetterCoinflips
{
    public class BetterCoinflips : Plugin<Config>
    {
        private static readonly Lazy<BetterCoinflips> LazyInstance = new Lazy<BetterCoinflips>(valueFactory: () => new BetterCoinflips());
        public static BetterCoinflips Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private Handlers.Player player;

        private BetterCoinflips()
        {

        }
        public override void OnEnabled()
        {
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
        }

        public void RegisterEvents()
        {
            player = new Handlers.Player();

            Player.FlippingCoin += player.OnFlippingCoing;
        }
        public void UnRegisterEvents()
        {
            Player.FlippingCoin -= player.OnFlippingCoing;

            player = null;
        }
    }
}