using System.Collections.Generic;
using UnityEngine;

public class ShieldRemoving : MonoBehaviour
{
    BossScript mainScript;
    [SerializeField] [Tooltip("Все щитки")]
    private List<GameObject> enabledShields = new List<GameObject>(); //изначально все включены

    private List<GameObject> disabledShields = new List<GameObject>(); //отключенные в данный момент щитки

    int numberOfOpenAreas;

    private void Awake()
    {
        mainScript = GetComponent<BossScript>();
    }

    private void OnEnable()
    {
        numberOfOpenAreas = enabledShields.Count - mainScript.numberOfShieldsLeft;
        for (int i = 0; i < numberOfOpenAreas; i++)
        {
            int index = Random.Range(0, enabledShields.Count); //номер щитка, который убираем
            disabledShields.Add(enabledShields[index]); //добавляем в отключенные
            enabledShields[index].gameObject.SetActive(false); //отключаем
            enabledShields.RemoveAt(index); //удаляем из включенных
        }
        //Debug.Log("On Enable. Enabled: " + enabledShields.Count + " Disabled: " + disabledShields.Count);
    }

    private void OnDisable()
    {
        for (int i = 0; i < disabledShields.Count; i++) //проходимся по отключенным, включаем их, удаляем из отключенных и добавляем во включенные
        {
            enabledShields.Add(disabledShields[i]);
            disabledShields[i].SetActive(true);
        }
        disabledShields.Clear();
        //Debug.Log("On Disable. Enabled: " + enabledShields.Count + " Disabled: " + disabledShields.Count);
    }
}
