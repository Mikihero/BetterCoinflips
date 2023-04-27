using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace BetterCoinflips.Commands.CoinUses
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CoinUses : ParentCommand
    {
        public CoinUses() => LoadGeneratedCommands();
        
        public override string Command { get; } = "coinuses";
        public override string[] Aliases { get; } = { };
        public override string Description { get; } = "Gets or sets the uses of a coin.";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Set.Set());
            RegisterCommand(new Get.Get());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("bc.coinuses"))
            {
                response = "You do not have permission to use this command";
                return false;
            }

            response = "Invalid subcommand. Available ones: set, get";
            return false;
        }

    }
}