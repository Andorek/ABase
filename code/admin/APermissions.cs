using System.Collections.Generic;

using ABase.Utilities;

namespace ABase.Admin;

public class APermissions
{
	private static Dictionary<string, APermissions> Groups {get; set;} = new() {
		["noperms"] = new() {
			CommandWhitelist = new() {}
		},
		["user"] = new() {
			CommandWhitelist = new() {

			}
		},
		["admin"] = new() {
			UseBlacklist = true,
			CommandBlacklist = new() {

			}
		},
		["operator"] = new() {
			UseBlacklist = true,
			CommandBlacklist = new() {}
		}
	};

	public static void InitPerms() {
		if (!ADataUtil.DataExists("AGroups"))
			ADataUtil.WriteData("AGroups", Groups);
	}

	public bool UseBlacklist {get; set;} = false;
	public List<string> CommandWhitelist {get; set;}
	public List<string> CommandBlacklist {get; set;}
}
