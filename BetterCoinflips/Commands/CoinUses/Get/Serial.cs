using System;
using System.Linq;
using CommandSystem;

namespace BetterCoinflips.Commands.CoinUses.Get
{
    public class Serial : ICommand
    {
        public string Command { get; } = "serial";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Gets the uses of a coin specified via serial number.";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Usage: coinuses get serial [serial]";
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
                response = $"Couldn't find a registered coin with the serial {serial}.";
                return false;
            }

            response = $"Coin with the serial number {serial} has {EventHandlers.CoinUses[serial]} uses left.";
            return true;
        }
    }
}