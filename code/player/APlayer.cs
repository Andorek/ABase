using System.Collections.Generic;
using Sandbox;
using ABase.Utilities;

namespace ABase.Player;

public partial class APlayer {

	public AGlobalConfig config;

	public APlayer() {
		ADataUtil.GetData(out config);
		// No id provided. Make a bot or something?
	}

	public APlayer(ref IClient cl) {
		ADataUtil.GetData(out config);
		var pawn = new APawn();

		List<ACharacterInfo> playerInfo = new();
		ACharacterInfo selectedChar = new();

		if (ADataUtil.DataExists("PlayerInfo")) { // The player has joined before.
			ADataUtil.GetData("PlayerInfo", cl.SteamId, out playerInfo);
			// Open a character selection window.
		}
		else { // The player has not joined before.
			// Open a character creation window.
		}

		pawn.CharacterInfo.SetCharInfo(selectedChar);
		cl.Pawn = pawn;
	}

}