using System.Collections.Generic;
using ABase.Utilities;

namespace ABase.Teams;

public class ATeam
{

    public static Dictionary<string, ATeam> TeamList {get; set;} = new() {
        ["Citizen"] = new() {
			CustomAllowed = () => {
				return true;
			}
		},
    };

    public ATeam() {

    }

    public static void InitTeams() {
        ATeam teams = new();
        if (!ADataUtil.DataExists<ATeam>())
            ADataUtil.WriteData(teams);
    }

    public List<string> RequiredGroup {get; set;} = new() {
        "user",
    };

    public delegate bool CustomAllowedDel();

	public CustomAllowedDel CustomAllowed {get; set;} = () => {return true;};

}
