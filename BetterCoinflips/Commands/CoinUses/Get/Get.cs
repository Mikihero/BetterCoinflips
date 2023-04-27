using System;
using CommandSystem;

namespace BetterCoinflips.Commands.CoinUses.Get
{
    public class Get : ParentCommand
    {
        public Get() => LoadGeneratedCommands();
        
        public override string Command { get; } = "get";
        public override string[] Aliases { get; } = { };
        public override string Description { get; } = "Gets the uses of a coin held by the specified player or by serial.";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Player());
            RegisterCommand(new Serial());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Invalid subcommand. Available ones: player, serial";
            return false;
        }
    }
}