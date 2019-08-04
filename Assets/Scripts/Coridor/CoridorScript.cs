using UnityEngine;
using UnityEngine.Tilemaps;

public class CoridorScript : MonoBehaviour
{
    public Transform spawnPoint;
    public int doorWidth;
    public Tilemap ground;
    public Tilemap walls;
    public Transform enterDoor;
    public Transform exitDoor;
    [HideInInspector]
    public Vector3 defaultExitDoorPosition;

    private void Awake()
    {
        defaultExitDoorPosition = exitDoor.localPosition;
    }

    private void OnDisable()
    {
        exitDoor.transform.localPosition = defaultExitDoorPosition;
    }
}
