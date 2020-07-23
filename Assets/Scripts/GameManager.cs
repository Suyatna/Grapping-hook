using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public Transform player;
    
    void Awake()
    {
        Instance = this;
        
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        Instantiate(player, new Vector2(-9, 0), Quaternion.identity);
    }
}
