using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager Instance;
    public GameObject currentCoridorSystem;
    [SerializeField] [Tooltip("Префаб коридора вместе с родительским объектом")]
    private GameObject coridorSystemPrefab;
    [SerializeField] [Tooltip("Куда коридоры складываем?")]
    private Transform coridorParent;
    [SerializeField] [Tooltip("Скорость, с которой открываем двери")]
    private float speed;

    CoridorScript oldCoridor;
    GameObject oldCoridorSystem;
    GameObject newCoridorSystem;
    CoridorScript newCoridor;
    [SerializeField] [Tooltip("Тайл земли")]
    private TileBase groundTile;

    private TileBase oldWallTile;
    private TileBase newWallTile;
    #region Singleton
    private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}
    #endregion

    public void LoadCoridor()
    {
        AddNewCoridor(currentCoridorSystem);
    }

    public void AddNewCoridor(GameObject _oldCoridorSystem)
    {
        oldCoridorSystem = _oldCoridorSystem;
        oldCoridor = oldCoridorSystem.GetComponentInChildren<CoridorScript>();
        newCoridorSystem = ObjectPoolingManager.Instance.GetObject(coridorSystemPrefab, coridorParent, oldCoridor.spawnPoint.position, ObjectPoolingManager.Instance.coridors);
        newCoridor = newCoridorSystem.GetComponentInChildren<CoridorScript>();
        currentCoridorSystem = newCoridorSystem;
        //oldWallTile = oldCoridor.walls.GetTile(Vector3Int.CeilToInt(oldCoridor.spawnPoint.position));
        //newWallTile = newCoridor.walls.GetTile(Vector3Int.CeilToInt(newCoridor.spawnPoint.position));
        StartCoroutine(OpenDoors());
        
    }

    private IEnumerator OpenDoors()
    {
        newCoridor.enterDoor.gameObject.SetActive(false);
        while (oldCoridor.exitDoor.transform.localPosition.x >= oldCoridor.transform.localPosition.x - oldCoridor.doorWidth)
        {
            Debug.Log("DoorOpen");
            oldCoridor.exitDoor.transform.localPosition = new Vector3(oldCoridor.exitDoor.transform.localPosition.x - speed * Time.deltaTime, oldCoridor.defaultExitDoorPosition.y, 0);
            yield return null;
        }
        StartCoroutine(CloseOldExitDoor());
    }

    private IEnumerator CloseOldExitDoor()
    {
        GameManager.Instance.level++;
		_SpawnDigit.Instance.oneNum = 0;
        while (oldCoridor.exitDoor.transform.localPosition.x < oldCoridor.defaultExitDoorPosition.x)
        {
            oldCoridor.exitDoor.transform.localPosition = new Vector3(oldCoridor.exitDoor.transform.localPosition.x + speed * Time.deltaTime, oldCoridor.defaultExitDoorPosition.y, 0);
            yield return null;
        }
        newCoridor.enterDoor.gameObject.SetActive(true);
        
        MoveToCoordBegin(coridorParent);
        Debug.Log("Yeeess");
        currentCoridorSystem.transform.SetParent(coridorParent);
        GameObject.FindWithTag("Player").transform.SetParent(currentCoridorSystem.transform);
        MoveToCoordBegin(currentCoridorSystem.transform);
        oldCoridorSystem.SetActive(false);

        EnemyManager.Instance.SpawnEnemies(GameManager.Instance.level);
    }

    public void MoveToCoordBegin(Transform objects)
    {
        objects.position = new Vector3(0, 0, 0);
       /* for (int i = 0; i < objects.Length; i++)
        {
            objects[i].position = Vector3.zero;
        } */
    }
}
