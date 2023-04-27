using System;
using System.Linq;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace BetterCoinflips.Commands.CoinUses.Set
{
    public class Serial : ICommand
    {
        public string Command { get; } = "serial";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Sets the uses of a coin specified by its serial number.";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("bc.coinuses.set"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            
            if (arguments.Count != 2)
            {
                response = "Usage: coinuses set serial [serial] [amount]";
                return false;
            }

            bool flag1 = ushort.TryParse(arguments.ElementAt(0), out ushort serial);
            if (!flag1)
            {
                response = $"Couldn't parse {arguments.ElementAt(0)} as serial.";
                return false;
            }

            if (!EventHandlers.CoinUses.ContainsKey(serial))
            {
                response = $"Couldn't find a coin with the serial {serial}.";
                return false;
            }

            bool flag2 = int.TryParse(arguments.ElementAt(1), out int amount);
            if (!flag2)
            {
                response = $"Couldn't parse {arguments.ElementAt(1)} as amount.";
                return false;
            }

            EventHandlers.CoinUses[serial] = amount;
            response = $"Successfully set the coins uses to {amount}.";
            return true;
        }
    }
}