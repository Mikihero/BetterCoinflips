using UnityEngine;
using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace BetterCoinflips
{
    public class EventHandlers : Config
    {
        public void OnCoinFlip(FlippingCoinEventArgs ev)
        {
            System.Random rd = new Random();
            int randomN = rd.Next(1, 6);
            if(!ev.IsTails)
            {
                switch(randomN)
                {
                    case 1:
                        Item.Create(ItemType.KeycardContainmentEngineer).Spawn(ev.Player.Position);
                        Map.Broadcast(BroadcastTime, "You acquired a Containment Engineer keycard!");
                        break;
                    case 2:
                        Item.Create(ItemType.Medkit).Spawn(ev.Player.Position);
                        Item.Create(ItemType.Painkillers).Spawn(ev.Player.Position);
                        Map.Broadcast(BroadcastTime, "You acquired a medic kit!");
                        break;
                    case 3:
                        ev.Player.Teleport(Door.Get(DoorType.EscapeSecondary));
                        Map.Broadcast(BroadcastTime, "Teleported to Escape");
                        break;
                    case 4:
                        ev.Player.Heal(25, true);
                        break;
                    case 5:
                        float hp = ev.Player.Health;
                        ev.Player.Health = hp * 3/2;
                        break;
                }
            }
            if(ev.IsTails)
            {
                switch(randomN)
                {
                    case 1:
                        float hp = ev.Player.Health;
                        ev.Player.Health = hp * 7/10;
                        Map.Broadcast(BroadcastTime, "Your health got reduced by 30%");
                        break;
                    case 2:
                        ev.Player.Teleport(Door.List.First(x => x.Type == DoorType.PrisonDoor).Position + Vector3.up);
                        Map.Broadcast(BroadcastTime, "You got teleported to a prison door?");
                        break;
                    case 3:
                        ev.Player.ApplyRandomEffect(10, false);
                        Map.Broadcast(BroadcastTime, "You got an random effect!");
                        break;
                    case 4:
                        if(Warhead.IsInProgress) {
                            Warhead.Stop();
                            Map.Broadcast(BroadcastTime, "The Alpha Warhead has been stopped!");
                        } else {
                            Warhead.Start();
                            Map.Broadcast(BroadcastTime, "The Alpha Warhead has been activated!");
                        }
                        break;
                    case 5:
                        Map.TurnOffAllLights(MapBlackoutTime);
                        break;
                }
            }
        }
    }
}
