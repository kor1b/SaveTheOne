using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance;

    [HideInInspector] public List<GameObject> coridors = new List<GameObject>();
	[HideInInspector] public List<GameObject> spikes = new List<GameObject>();
	[HideInInspector] public List<GameObject> enemies = new List<GameObject>();
	[HideInInspector] public List<GameObject> bosses = new List<GameObject>();
	[HideInInspector] public List<GameObject> bullets = new List<GameObject>();

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    public GameObject GetObject(GameObject obj, Transform parent, Vector2 position, List<GameObject> list)
    {
        
        for (int i = 0; i < list.Count; i++) //ищем доступный для переиспользования объект
        {
            if (!list[i].activeInHierarchy)
            {
                list[i].transform.position = position;
                list[i].SetActive(true);
                //list[i].transform.SetParent(parent);
                return list[i];
            }
        }
        //если таких нет
        GameObject newObj = Instantiate(obj);
        newObj.transform.position = position;
        
        newObj.transform.SetParent(parent);
        list.Add(newObj);
        return newObj;
    }
}
