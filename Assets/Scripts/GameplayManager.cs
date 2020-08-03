using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Manager.isLoadScene)
        {
            LoadSlot(GameManager.Manager.slot);
        }
        
        Debug.Log("isLoaded: " +GameManager.Manager.isLoadScene);
        Debug.Log("slot: " +GameManager.Manager.slot);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;
    }
    
    public void LoadSlot(string slot)
    {
        SaveSystem.LoadSlot = "/hook" + slot + ".fun";
        LoadPlayer();
    }
}
