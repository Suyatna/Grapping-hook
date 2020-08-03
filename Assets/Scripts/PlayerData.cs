using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int saveSceneIndex;
    public float[] position;

    public PlayerData(Player player)
    {
        saveSceneIndex = player.saveSceneIndex;
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
