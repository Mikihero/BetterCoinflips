using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace BetterCoinflips.Commands.CoinUses.Set
{
    public class Set : ParentCommand
    {
        public Set() => LoadGeneratedCommands();
        
        public override string Command { get; } = "set";
        public override string[] Aliases { get; } = { };
        public override string Description { get; } = "Sets the amount of uses of a coin held by the specified player or by serial.";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Player());
            RegisterCommand(new Serial());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("bc.coinuses.set"))
            {
                response = "You do not have permission to use this command";
                return false;
            }
            
            response = "Invalid subcommand. Available ones: player, serial";
            return false;
        }
    }
}