using Exiled.API.Features;
using System;
using HarmonyLib;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;
using Map = Exiled.Events.Handlers.Map;
using Server = Exiled.Events.Handlers.Server;

namespace BetterCoinflips
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override Version RequiredExiledVersion => new Version(5, 3, 0, 0);
        public override Version Version => new Version(1, 2, 1);
        public override string Author => "Miki_hero";

        private EventHandlers _eventHandler;
        private Harmony _harmony;
        
        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
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

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _eventHandler = new EventHandlers();
            Player.FlippingCoin += _eventHandler.OnCoinFlip;
        }

        private void UnRegisterEvents()
        {
            Player.FlippingCoin -= _eventHandler.OnCoinFlip;
            _eventHandler = null;
        }
    }
}