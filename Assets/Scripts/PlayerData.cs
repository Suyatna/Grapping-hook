[System.Serializable]
public class PlayerData
{
    public int[] slot;

    public PlayerData(int[] player)
    {
        slot = new int[5];
        slot = player;
    }
}
