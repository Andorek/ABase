using System.Collections.Generic;
using Sandbox;
using ABase.Utilities;

namespace ABase.Player;

public partial class APlayer {

	public AGlobalConfig config;

	public APlayer() {
		// No id provided. Make a bot or something?
	}

	// charIndex allows you to choose which character the player should load up as. Useful if they have multiple characters from when AllowMultipleCharacters was true, but then changed to false.
	public APlayer(IClient cl, int charIndex = 0) {
		ADataUtil.GetData(out config);
		var pawn = new APawn();

		List<ACharacterInfo> playerInfo = new();

		if (ADataUtil.DataExists("PlayerInfo")) { // The player has joined before.
			ADataUtil.GetData("PlayerInfo", cl.SteamId, out playerInfo);
			if (!config.AllowMultipleCharacters) {
				pawn.CharacterInfo = playerInfo[charIndex];
			}
		}
		else { // The player has not joined before.

		}
	}

}
