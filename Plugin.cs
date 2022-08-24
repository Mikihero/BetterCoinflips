using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
using Map = Exiled.Events.Handlers.Map;

namespace BetterCoinflips
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override Version RequiredExiledVersion => new Version(5, 3, 0);
        public override Version Version => new Version(1, 0, 0);
        public override string Author => "Miki_hero";
        private EventHandlers EventHandler;

        public override void OnEnabled()
        {
            Instance = this;
            try { RegisterEvents(); }
            catch(Exception ex) { Log.Error($"Failed to load \"BetterCoinflips\": {ex}");}
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            EventHandler = new EventHandlers();
            Player.FlippingCoin += EventHandler.OnCoinFlip;
            Map.SpawningItem += EventHandler.OnSpawningItem;

        }
        public void UnRegisterEvents()
        {
            Player.FlippingCoin -= EventHandler.OnCoinFlip;
            Map.SpawningItem -= EventHandler.OnSpawningItem;
            EventHandler = null;
        }
    }
}