using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;
using Random = System.Random;
using Exiled.API.Features.Items;

namespace BetterCoinflips.Handlers
{
    class Player
    {
        public void OnFlippingCoing(FlippingCoinEventArgs ev)
        {
            if(ev.IsTails == false)
            {
                Random rd = new Random();
                int headsEvent = rd.Next(1, 6);
                if(headsEvent == 1)
                {
                    Item.Create(ItemType.KeycardContainmentEngineer).Spawn(ev.Player.Position);
                    Map.Broadcast(2, "karta");
                }
                if(headsEvent == 2)
                {
                    Item.Create(ItemType.Medkit).Spawn(ev.Player.Position);
                    Item.Create(ItemType.Painkillers).Spawn(ev.Player.Position);
                    Map.Broadcast(2, "medyk");
                }
                if(headsEvent == 3)
                {
                    //ev.Player.Teleport(Door.List.First(x => x.Type == DoorType.EscapeSecondary).Position + Vector3.up);
                    ev.Player.Teleport(Door.Get(DoorType.EscapeSecondary));
                    Map.Broadcast(2, "tp do escape");
                }
                if(headsEvent == 4)
                {
                    ev.Player.Heal(25, true);
                }
                if(headsEvent == 5)
                {
                    float hp = ev.Player.Health;
                    ev.Player.Health = hp * 3/2;
                }
            }
            if (ev.IsTails == true)
            {
                Random rd = new Random();
                int tailsEvent = rd.Next(1, 6);
                if(tailsEvent == 1)
                {
                    float hp = ev.Player.Health;
                    ev.Player.Health = hp * 7/10;
                    Map.Broadcast(2, "70% hp");
                }
                if(tailsEvent == 2)
                {
                    ev.Player.Teleport(Door.List.First(x => x.Type == DoorType.PrisonDoor).Position + Vector3.up);
                    Map.Broadcast(2, "tp to prison door????");
                }
                if(tailsEvent == 3)
                {
                    ev.Player.ApplyRandomEffect(10, false);
                    Map.Broadcast(2, "random effect");
                }
                if(tailsEvent == 4)
                {
                    if(Warhead.IsInProgress == true)
                    {
                        Warhead.Stop();
                    }
                    else
                    {
                        Warhead.Start();
                    }
                    Map.Broadcast(2, "warhead");
                }
                if(tailsEvent == 5)
                {
                    Map.TurnOffAllLights(10);
                }
            }
        }
    }
}
