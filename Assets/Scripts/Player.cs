using UnityEngine;

public class Player : MonoBehaviour
{
    public int saveSceneIndex;
    
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void SaveSlot(string slot)
    {
        SaveSystem.SaveSlot = "/hook" + slot + ".fun";
        SavePlayer();
    }
}
