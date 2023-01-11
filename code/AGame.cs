using Sandbox;
using ABase.Player;
using ABase.Utilities;

namespace ABase;

public partial class AGame : GameManager
{
	public AGame()
	{
		// Initialise everything
		if (Game.IsServer) {
			ADataUtil.InitData();
			AGlobalConfig.InitConfig();
		}
	}

	// A client has joined the server. Make them a pawn to play with
	public override void ClientJoined( IClient client )
	{
		base.ClientJoined( client );

		// Initialise the player
		var ply = new APlayer(ref client);
	}
}
