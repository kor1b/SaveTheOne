using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SpawnDigit : MonoBehaviour {
    public static _SpawnDigit Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    public GameObject[] digits;
    GameObject digit;
    public GameObject Parent;
    public bool isGameOver;
    public float timeForWait;
    int digitNumber;                //количество чисел в ленте (от 0 до 5+-)
    List<CDigit> cDigits;
    int digitActive;
    [SerializeField]
    Sprite[] spriteDigit;
    void Start () {
        cDigits = new List<CDigit>();
        digitActive = 0;
        StartCoroutine(Spawn_Digit());
        
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            SetDigitActive();
        }
	}

    IEnumerator Spawn_Digit()
    {
        int i = 0;
        while (!isGameOver)
        {
            yield return new WaitForSeconds(timeForWait);

            if (cDigits.Count<5)
            {
                int rand = Random.Range(0, 8);
               // yield return new WaitForSeconds(timeForWait);
                digit = Instantiate(digits[rand], Parent.transform.position, digits[rand].transform.rotation, Parent.transform);
                cDigits.Add(new CDigit(digit,rand+1));
            }
            else
            {
                //isGameOver = true;
            }
        }
    }
    
    public int GetCDigitsLength()
    {
        return cDigits.Count;
    }
    public void DeleteDigit()
    {
        if (cDigits.Count > 0)
        {
            int n = digitActive;
            if (digitActive == cDigits.Count - 1)
            {
                digitActive = 0;
            }
            Destroy(cDigits[n].gameObjectDigit);
            cDigits.RemoveAt(n);
        }
    }
    public void SetDigitActive()
    {
        if (cDigits.Count > 0)
        {
            if (digitActive == cDigits.Count - 1)
            {
                digitActive = 0;
            }
            else
            {
                digitActive++;
            }
        }
    }
    public int GetDigitActive()
    {
        return cDigits[digitActive]._digit-1;
    }
    public Sprite GetSpriteActiveDigit()
    {
        return spriteDigit[GetDigitActive()];
    }
}
[System.Serializable]
class CDigit
{
    public GameObject gameObjectDigit;
    public int _digit;
    public CDigit(GameObject god, int d)
    {
        this.gameObjectDigit = god;
        this._digit = d;
    }
}