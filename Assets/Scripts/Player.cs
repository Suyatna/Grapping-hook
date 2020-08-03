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
    
    // public void LoadPlayer()
    // {
    //     PlayerData data = SaveSystem.LoadPlayer();
    //     
    //     Vector3 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];
    //     position.z = data.position[2];
    //
    //     transform.position = position;
    // }
    //
    // public void LoadSlot(string slot)
    // {
    //     SaveSystem.LoadSlot = "/hook" + slot + ".fun";
    //     LoadPlayer();
    // }
}
