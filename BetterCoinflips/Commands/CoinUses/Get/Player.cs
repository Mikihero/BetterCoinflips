using System;
using System.Linq;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace BetterCoinflips.Commands.CoinUses.Get
{
    public class Player : ICommand
    {
        public string Command { get; } = "player";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Gets the uses of a coin held by the specified player.";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("bc.coinuses.get"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            
            if (arguments.Count == 0)
            {
                Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(sender);
                if (player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
                {
                    response = "You are not holding a coin right now.";
                    return false;
                }
                
                if (!EventHandlers.CoinUses.ContainsKey(player.CurrentItem.Serial))
                {
                    response = $"Your held coin isn't registered because it wasn't used yet.";
                    return false;
                }
                
                response = $"Your held coin has {EventHandlers.CoinUses[player.CurrentItem.Serial]} uses left.";
                return true;
            }

            if (arguments.Count == 1)
            {
                Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(arguments.ElementAt(0));
                if (player == null)
                {
                    response = $"Couldn't parse {arguments.ElementAt(0)} as a valid target.";
                    return false;
                }
                if (player.CurrentItem == null || player.CurrentItem.Type != ItemType.Coin)
                {
                    response = $"{player.Nickname} is not holding a coin right now.";
                    return false;
                }

                if (!EventHandlers.CoinUses.ContainsKey(player.CurrentItem.Serial))
                {
                    response = $"{player.Nickname}'s held coin isn't registered because it wasn't used yet.";
                    return false;
                }
                
                response = $"{player.Nickname}'s held coin has {EventHandlers.CoinUses[player.CurrentItem.Serial]} uses left.";
                return true;
            }
            
            response = "Usage: coinuses get player [id/name]";
            return false;
        }
    }
}