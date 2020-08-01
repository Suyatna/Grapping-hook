using UnityEngine.Serialization;

[System.Serializable]
public class PlayerData
{
    public int level;

    public PlayerData(int level)
    {
        this.level = level;
    }
}
