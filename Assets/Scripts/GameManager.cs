using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; private set; }

    public string slot;
    
    public bool isLoadScene;

    private void Awake()
    {
        if (Manager == null)
        {
            Manager = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
