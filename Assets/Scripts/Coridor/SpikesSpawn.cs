using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesSpawn : MonoBehaviour
{
    [SerializeField] [Tooltip("Тайл шипов")]
    private TileBase tile;
    private Tilemap tilemap;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnEnable()
    {
        tilemap.SetTile(new Vector3Int(-8, -4, 0), tile);
    }
}
