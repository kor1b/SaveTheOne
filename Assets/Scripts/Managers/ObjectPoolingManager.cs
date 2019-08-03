using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance;
    [Header("Prefabs")]
    [SerializeField] [Tooltip("Префаб коридора")]
    private GameObject coridorPrefab;
    [SerializeField] [Tooltip("Префаб шипа")]
    private GameObject spikePrefab;
    [SerializeField] [Tooltip("Префаб врага")]
    private GameObject enemyPrefab;
    [SerializeField] [Tooltip("Префаб босса")]
    private GameObject bossPrefab;
    [SerializeField] [Tooltip("Префаб пуль")]
    private GameObject bulletPrefab;
    [Header("Parents")]
    [SerializeField] [Tooltip("Родительский объект коридоров")]
    private Transform coridorParent;
    [SerializeField] [Tooltip("Родительский объект врагов")]
    private Transform enemyParent;
    [SerializeField] [Tooltip("Родительский объект пуль")]
    private Transform bulletParent;

    private List<GameObject> coridors = new List<GameObject>();
    private List<GameObject> spikes = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> bosses = new List<GameObject>();
    private List<GameObject> bullets = new List<GameObject>();

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    public GameObject GetCorridor()
    {
        GameObject newObj = FindUnactive(coridors); //ищем доступный для переиспользования объект
        if (newObj == null) //если таких нет
        {
            newObj = Instantiate(coridorPrefab);
            coridors.Add(newObj);
        }
        newObj.transform.SetParent(coridorParent);
        coridors.Add(newObj);
        return newObj;
    }
    public GameObject GetSpike(Transform parent, Vector2 position)
    {
        GameObject newObj = FindUnactive(spikes); //ищем доступный для переиспользования объект
        if (newObj == null) //если таких нет
        {
            newObj = Instantiate(spikePrefab);
            spikes.Add(newObj);
        }
        newObj.transform.SetParent(parent);
        newObj.transform.position = position;
        return newObj;
    }

    public GameObject GetEnemy(Vector2 position)
    {
        GameObject newObj = FindUnactive(enemies); //ищем доступный для переиспользования объект
        if (newObj = null) //если таких нет
        {
            newObj = Instantiate(enemyPrefab);
            enemies.Add(newObj);
        }
        newObj.transform.SetParent(enemyParent);
        newObj.transform.position = position;
        return newObj;
    }
    public GameObject GetBoss(Vector2 position)
    {
        GameObject newObj = FindUnactive(bosses); //ищем доступный для переиспользования объект
        if (newObj = null) //если таких нет
        {
            newObj = Instantiate(bossPrefab);
            bosses.Add(newObj);
        }
        newObj.transform.SetParent(enemyParent);
        newObj.transform.position = position;
        return newObj;
    }

    public GameObject GetBullet(Vector2 position)
    {
        GameObject newObj = FindUnactive(bullets); //ищем доступный для переиспользования объект
        if (newObj = null) //если таких нет
        {
            newObj = Instantiate(bulletPrefab);
            bosses.Add(newObj);
        }
        newObj.transform.SetParent(bulletParent);
        newObj.transform.position = position;
        return newObj;
    }

    private GameObject FindUnactive(List<GameObject> list) //ищем доступный для переиспользования объект
    {
        for (int i = 0; i < list.Count; i++) 
        {
            if (!list[i].activeInHierarchy)
            {
                list[i].SetActive(true);
                return list[i];
            }
        }
        return null;
    }
}
