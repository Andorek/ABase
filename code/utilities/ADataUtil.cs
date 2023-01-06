using System.Collections;
using System.Collections.Generic;
using Sandbox;
namespace ABase.Utilities;

public static class ADataUtil
{
	// key should be the 'name' of the config, while the value should be the path.
	private static Dictionary<string, string> DataReg;

	public static void InitData() {
		if (!FileSystem.Data.FileExists("DataReg.json")) {
			DataReg = new();
			FileSystem.Data.WriteJson("DataReg.json", DataReg);
		}
		
		DataReg = FileSystem.Data.ReadJson<Dictionary<string, string>>("DataReg.json");
	}

	private static void RegisterData(string name, string path = null) {
		if (path == null) return;

		if (DataReg.ContainsKey(name) )
			DataReg[name] = path + name + ".json";
		else if (!DataReg.ContainsKey(name))		
			DataReg.Add(name, path + name + ".json");

		FileSystem.Data.WriteJson("DataReg.json", DataReg);
	}


	public static void WriteData<T>(string name, T data, string path = null) {
		RegisterData(name, path);
		FileSystem.Data.WriteJson(DataReg[name], data);
	}

	public static void WriteData<T>(T data, string path = "") {
		string name = typeof(T).Name;
		RegisterData(name, path);
		FileSystem.Data.WriteJson(DataReg[name], data);
	}

	public static void WriteData<T1, T2>(string name, T1 key, T2 data, string path = null) {
		RegisterData(name, path);

		Dictionary<T1, T2> newData = new();
		if (DataExists(name))
			newData = FileSystem.Data.ReadJson<Dictionary<T1, T2>>(DataReg[name]);

		newData[key] = data;
		
		FileSystem.Data.WriteJson(DataReg[name], newData);
	}

	public static void WriteData<T1, T2>(T1 key, T2 data, string path = null) {
		string name = nameof(T2);
		RegisterData(name, path);

		Dictionary<T1, T2> newData = new();
		if (DataExists(name))
			newData = FileSystem.Data.ReadJson<Dictionary<T1, T2>>(DataReg[name]);

		newData[key] = data;
		
		FileSystem.Data.WriteJson(DataReg[name], newData);
	}


	public static void DeleteData(string name) {
		if (!DataExists(name)) return;

		FileSystem.Data.DeleteFile(DataReg[name]);
		DataReg.Remove(name);
		FileSystem.Data.WriteJson("DataReg.json", DataReg);
	}

	public static void DeleteData<T1, T2>(string name, T1 key, T2 data) { // Data can be 'default', we just need the type because FileSystem.Data.ReadJson can't deserialise a Dictionary<T1, object>.
		if (!DataExists(name)) return;

		Dictionary<T1, T2> newData = FileSystem.Data.ReadJson<Dictionary<T1, T2>>(DataReg[name]);
		newData.Remove(key);

		FileSystem.Data.WriteJson(DataReg[name], newData);

	}

	public static void DeleteData<T1, T2>(T1 key, T2 data) {
		string name = nameof(T2);
		if (!DataExists(name)) return;

		Dictionary<T1, T2> newData = FileSystem.Data.ReadJson<Dictionary<T1, T2>>(DataReg[name]);


		FileSystem.Data.DeleteFile(DataReg[name]);
		DataReg.Remove(name);
		FileSystem.Data.WriteJson("DataReg.json", DataReg);

	}


	public static void GetData<T>(string name, out T data) {
		if (!DataReg.ContainsKey(name)) {
			data = default;
			return;
		}

		data = FileSystem.Data.ReadJson<T>(DataReg[name]);
	}

	public static void GetData<T>(out T data) {
		string name = nameof(T);
		if (!DataReg.ContainsKey(name)) {
			data = default;
			return;
		}

		data = FileSystem.Data.ReadJson<T>(DataReg[name]);
	}

	public static void GetData<T1, T2>(string name, T1 key, out T2 data) {
		if (!DataReg.ContainsKey(name)) {
			data = default;
			return;
		}
		
		data = FileSystem.Data.ReadJson<Dictionary<T1, T2>>(DataReg[name])[key];
	}

	public static void GetData<T1, T2>(T1 key, out T2 data) {
		string name = nameof(T2);
		if (!DataReg.ContainsKey(name)) {
			data = default;
			return;
		}
		
		data = FileSystem.Data.ReadJson<Dictionary<T1, T2>>(DataReg[name])[key];
	}


	public static bool DataExists(string name) {
		if (DataReg.TryGetValue(name, out string path) && FileSystem.Data.FileExists(path ?? "")) 
			return true;

		return false;
	}
}
