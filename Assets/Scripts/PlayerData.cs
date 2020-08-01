[System.Serializable]
public class PlayerData
{
    public int[] slot;

    public PlayerData(int[] index)
    {
        slot = new int[5];
        slot = index;
    }
}
