using System.Collections.Generic;
using ABase.Utilities;
using ABase.Admin;

namespace ABase;

public class AGlobalConfig
{
	public static void InitConfig() {
		AGlobalConfig config = new();
		if (ADataUtil.DataExists("AGlobalConfig"))
			ADataUtil.GetData("AGlobalConfig", out config);
		
		ADataUtil.WriteData(config);
	}

	/*
		Settings and their default values under here.
	*/

	// Default speed is multiplied by the values in 'speedStates.'
	public float DefaultSpeed {get; set;} = 10f;
	public Dictionary<string, float> SpeedStates {get; set;} = new() {
		["crouching"] = 0.5f,
		["walking"] = 1.0f,
		["running"] = 2.0f
	};
	public string DefaultSpeedState {get; set;} = "walking";

	public bool AllowMultipleCharacters {get; set;} = true;
	public int DefaultMaxCharacters {get; set;} = 3;

}
