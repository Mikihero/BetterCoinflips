using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features.Items;
using Exiled.Permissions.Extensions;

namespace BetterCoinflips.Commands.CoinUses.Set
{
    public class Player : ICommand
    {
        public string Command { get; } = "player";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Sets the uses of a coin held by the specified player.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("bc.coinuses.set"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            
            if (arguments.Count == 1)
            {
                Item coin = GetCoinByPlayer(Exiled.API.Features.Player.Get(sender));
                if (coin == null)
                {
                    response = "You are not holding a coin.";
                    return false;
                }
                
                bool flag1 = int.TryParse(arguments.ElementAt(0), out int amount);
                
                if (!flag1)
                {
                    response = $"Couldn't parse {arguments.ElementAt(0)} as amount.";
                    return false;
                }

                EventHandlers.CoinUses[coin.Serial] = amount;
                response = $"Successfully set the coins uses to {amount}.";
                return true;
            }

            if (arguments.Count == 2)
            {
                Exiled.API.Features.Player target = Exiled.API.Features.Player.Get(arguments.ElementAt(0));
                if (target == null)
                {
                    response = $"Couldn't parse {arguments.ElementAt(0)} as a valid target.";
                    return false;
                }
                
                Item coin = GetCoinByPlayer(target);
                if (coin == null)
                {
                    response = $"{target.Nickname} is not holding a coin.";
                    return false;
                }
                
                bool flag1 = int.TryParse(arguments.ElementAt(1), out int amount);
                
                if (!flag1)
                {
                    response = $"Couldn't parse {arguments.ElementAt(1)} as amount.";
                    return false;
                }

                EventHandlers.CoinUses[coin.Serial] = amount;
                response = $"Successfully set the coins uses to {amount}.";
                return true;
            }

            response = "Usage: coinuses set player [id/name] [amount]\nOR: coinuses set player amount";
            return false;
        }

        private Item GetCoinByPlayer(Exiled.API.Features.Player pl)
        {
            return pl.CurrentItem != null && pl.CurrentItem.Type == ItemType.Coin ? pl.CurrentItem : null;
        }
    }
}