using System;
using System.Collections.Generic;
using Sandbox;
using ABase.Player;

namespace ABase.Admin;

public class ACommand
{
	private static Dictionary<string, ACommand> Commands = new() {
		["kick"] = new() {

			// Nice looking name to use in gui etc.
			PrintName = "Kick",

			// Brief description of what the command does.
			Description = "Kicks the player from the server.",

			// Define and cast the arguments of the delegate in this method body, as well as defining the behaviour of the command.
			// For example, to run this command in this class to kick player "Mike Oxenpayne" you would do:
			// Commands["kick"].Action("Mike Oxenpayne")
			Action = args => {

				string target = (string)args[1]; // Define and cast the first argument as 'target'. This will be the person who is kicked or banned, and can be either their pseudoName or SteamID.

				foreach (var client in Game.Clients) {
					if (client.Pawn is not APawn pawn)
						continue;

					// The target can be either the player's steamid or their pseudoname.
					if (pawn.CharacterInfo.PseudoName == target )
					if (target == client.SteamId.ToString() || target == pawn.CharacterInfo.PseudoName) {

					}
					
				}
			},
		},
		["ban"] = new() {

		},
		["freeze"] = new() {

		},
		["jail"] = new() {

		},
	};

	public string PrintName = "Fancy name";
	public string Description = "A simple command!";

	public delegate void CommandAction(params object[] args);
	public CommandAction Action;

	[ConCmd.Server] // This needs a workaround as ServerRPCs don't exist yet. That's why args should be
	public static void RunCommand(string command, params string[] args) {
		var caller = ConsoleSystem.Caller;
		// TODO: Check if the caller has the right permissions to run the command.

		Commands[command].Action(args);
	}
 
}
