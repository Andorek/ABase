using System.Collections.Generic;
using Sandbox;
using ABase.Utilities;

namespace ABase.Player;

public partial class APlayer
{
	public APawn[] pawns;
	public AGlobalConfig config;

	public APlayer() {
		ADataUtil.GetData(out config);
		pawns = new APawn[config.DefaultMaxCharacters];

		Dictionary<long, ACharacterInfo> playerInfo = new();

		if (ADataUtil.DataExists("PlayerInfo")) { // The player has joined before.
			ADataUtil.GetData("PlayerInfo", out playerInfo);
		}
		else { // The player has not joined before.

		}

		
	}
}
