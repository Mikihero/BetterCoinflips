using Exiled.API.Features;
using System;
using Exiled.Events.EventArgs;
using Player = Exiled.Events.Handlers.Player;


namespace BetterCoinflips
{
    public class BetterCoinflips : Plugin<Config>
    {
        private EventHandlers EventHandler;

        public override void OnEnabled()
        {
            try { RegisterEvents(); }
            catch(Exception ex) { Log.Error($"Failed to load \"BetterCoinflips\": {ex}");}
        }

        public override void OnDisabled()
        {
            UnRegisterEvents();
        }

        public void RegisterEvents()
        {
            EventHandler = new EventHandlers();
            Player.FlippingCoin += EventHandler.OnCoinFlip;
            base.OnEnabled();
        }
        public void UnRegisterEvents()
        {
            Player.FlippingCoin -= EventHandler.OnCoinFlip;
            EventHandler = null;
            base.OnDisabled();
        }
    }
}