using System;
using System.Collections.Generic;
using Sandbox;
using ABase.Player;
using ABase.Utilities;

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
			// Commands["kick"].Action("Mike Oxenpayne"), OR:
			// Commands["kick"].Action("00000000000000000"), (Mike's steamID64)
			Action = args => {

				string target = (string)args[0]; // Define and cast the first argument as 'target'. This will be the person who is kicked or banned, and can be either their pseudoName or SteamID.

				foreach (var client in Game.Clients) {
					if (client.Pawn is not APawn pawn)
						continue;

					// The target can be either the player's steamid or their pseudoname.
					// Currently if someone makes their psuedoName someone elses steam ID, they could be incorrectly targeted for a ban.
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

	public delegate void CommandDel(params object[] args);
	public CommandDel Action;


	[ConCmd.Server] // This needs a workaround as ServerRPCs don't exist yet, and so we can't just network plain objects.
	public static void RunCommand(string command, params string[] stringArgs) {
		var caller = ConsoleSystem.Caller;
		if (!CanUseCommand(command, caller)) return;


		// I have no idea if the following is necessary
		object[] args = new object[stringArgs.Length];
		for (int i = 0; i < stringArgs.Length; i++) {
			args[i] = stringArgs[i];
		}

		Commands[command].Action(args);
	}

	public static bool CanUseCommand(string command, IClient client = null) {
		//client ??= ConsoleSystem.Caller;
		//if (client == null) return false;

		//ADataUtil.GetData("AGroups", out Dictionary<string, APermissions> groups);

		//APawn pawn = client.Pawn as APawn;
		//var plyGroup = groups[pawn.CharacterInfo.Team];
		/*if (plyGroup.UseBlacklist) {
			//return !plyGroup.CommandBlacklist.Contains(command);
		}
		else {
			//return plyGroup.CommandWhitelist.Contains(command);
		}*/
		return true;
	}

	public static bool CanUseCommand(string command, long id) {
		IClient client = null;
		foreach(IClient cl in Game.Clients) {
			if (cl.Id == id)
				client = cl;
		}
		if (client == null) return false;

		ADataUtil.GetData("AGroups", out Dictionary<string, APermissions> groups);

		APawn pawn = client.Pawn as APawn;
		var plyGroup = groups[pawn.CharacterInfo.Team];
		if (plyGroup.UseBlacklist) {
			return !plyGroup.CommandBlacklist.Contains(command);
		}
		else {
			return plyGroup.CommandWhitelist.Contains(command);
		}
	}
 
}
