using ABase.Utilities;

namespace ABase.Player;

public class ACharacterInfo
{
  private AGlobalConfig config = new();

  public string PseudoName {get; private set;} = "Mike Oxenpayne";
  public float Money {get; private set;} = 0; // Just for now. Implement a bank system?
  public string Team {get; private set;} = "Citizen";

  public ACharacterInfo() {
    ADataUtil.GetData(out config);
  }

  public void SetCharInfo(ACharacterInfo c) {
    UpdatePsuedoName(c.PseudoName);
    SetMoney(c.Money);
    ChangeTeam(c.Team);
  }

  public void UpdatePsuedoName(string name) {
	Log.Info(config.MaxPseudoNameLength);
    if (name.Length > config.MaxPseudoNameLength) return;
    foreach(string word in config.BannedNames) {
      if (name.Contains(word)) return;
    }

    PseudoName = name;
  }


  // Placeholder methods //
  public void SetMoney(float amount) {
    if (amount < 0) amount = 0;
    Money = amount;
  }

  public void AddMoney(float amount) {
    float result = Money + amount;
    if (result < 0) result = 0;
    Money += result;
  }

  public void ChangeTeam(string team) {
    Team = team;

    // Check if the team exists first.
  }
}
