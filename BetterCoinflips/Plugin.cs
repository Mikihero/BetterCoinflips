using Exiled.API.Features;
using System;
using HarmonyLib;
using Map = Exiled.Events.Handlers.Map;
using Player = Exiled.Events.Handlers.Player;

namespace BetterCoinflips
{
    public class Plugin : Plugin<Config, Translations>
    {
        public static Plugin Instance;
        public override Version RequiredExiledVersion => new(6,1,0);
        public override Version Version => new(2, 1,1);
        public override string Author => "Miki_hero";
        public override string Name => "BetterCoinflips";
        public override string Prefix => "better_cf";

        private EventHandlers _eventHandler;
        private Harmony _harmony;
        
        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
            Patch();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnPatch();
            UnRegisterEvents();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _eventHandler = new EventHandlers();
            Player.FlippingCoin += _eventHandler.OnCoinFlip;
            Map.SpawningItem += _eventHandler.OnSpawningItem;
            Player.InteractingDoor += _eventHandler.OnInteractingDoorEventArgs;
        }

        private void UnRegisterEvents()
        {
            Player.FlippingCoin -= _eventHandler.OnCoinFlip;
            Map.SpawningItem -= _eventHandler.OnSpawningItem;
            Player.InteractingDoor -= _eventHandler.OnInteractingDoorEventArgs;
            _eventHandler = null;
        }

        private void Patch()
        {
            try
            {
                _harmony = new Harmony("bettercoinflips.patch");
                _harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to patch: {ex}");
                _harmony.UnpatchAll();
            }
        }

        private void UnPatch()
        {
            _harmony.UnpatchAll();
            _harmony = null;
        }
    }
}