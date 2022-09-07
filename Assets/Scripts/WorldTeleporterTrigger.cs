using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldTeleporterTrigger : MonoBehaviour
{
    public int World;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainChar>() != null )
        {
            if(GameData.IsWorldUnlocked(World) && World != GameData.CurrentWorld)
            {
                GameData.CurrentDoor = 0;
                GameData.SetWorld(World);
                SceneManager.LoadScene("HubScene" + (World == 1 ? 1 : 2));
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = new Color(0.75f, 0.5f, 1, 0.5f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.color = new Color(0.5f, 0, 1, 0.7f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
