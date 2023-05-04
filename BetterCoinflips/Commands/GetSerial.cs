using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Permissions.Extensions;

namespace BetterCoinflips.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GetSerial : ICommand, IUsageProvider
    {
        public string Command { get; } = "getserial";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Gets the serial number of an item.";
        public string[] Usage { get; } = { "id/name (Optional)"};
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("bc.getserial"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            if (arguments.Count == 0)
            {
                Item item = Player.Get(sender).CurrentItem;
                if (item == null)
                {
                    response = "You're not holding any items.";
                    return false;
                }

                response = $"Item: {item.Base.name}, Serial: {item.Serial}";
                return true;
            }

            if (arguments.Count == 1)
            {
                Player player = Player.Get(arguments.ElementAt(0));
                if (player == null)
                {
                    response = $"Player {arguments.ElementAt(0)} not found.";
                    return false;
                }

                Item item = player.CurrentItem;
                if (item == null)
                {
                    response = "The specified player is not holding any items.";
                    return false;
                }

                response = $"Item: {item.Base.name}, Serial: {item.Serial}";
                return true;
            }

            response = "Incorrect usage.";
            return false;
        }
    }
}