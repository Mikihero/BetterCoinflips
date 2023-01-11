using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
using Map = Exiled.Events.Handlers.Map;

namespace BetterCoinflips
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override Version RequiredExiledVersion => new Version(5, 3, 0, 0);
        public override Version Version => new Version(1, 2, 1);
        private EventHandlers _eventHandler;

        public override void OnEnabled()
        {
            Instance = this;
            try
            {
                RegisterEvents();
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to load \"BetterCoinflips\": {ex}");
            }
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            Instance = null;
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            _eventHandler = new EventHandlers();
            Player.FlippingCoin += _eventHandler.OnCoinFlip;
            Map.SpawningItem += _eventHandler.OnSpawningItem;

        }
        public void UnRegisterEvents()
        {
            Player.FlippingCoin -= _eventHandler.OnCoinFlip;
            Map.SpawningItem -= _eventHandler.OnSpawningItem;
            _eventHandler = null;
        }
    }
}