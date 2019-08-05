using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SpawnDigit : MonoBehaviour
{

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
	//Sprite[] spriteActiveDigit;
	public int minOneNumCount = 2;
	public int maxOneNumCount = 10;
	[HideInInspector] public int oneNum = 0;
	bool lastFrontier;
	int maxLevelDigit;

	void Start()
	{
		lastFrontier = false;
		cDigits = new List<CDigit> ();
		oneNum = 0;
		int rand = Random.Range (0, 9);
		if (rand == 0)
		{
			oneNum++;
		}
		digit = Instantiate (digits[rand], Parent.transform.position + new Vector3 (6.5f, 0f, 0f), digits[rand].transform.rotation, Parent.transform);
		cDigits.Add (new CDigit (digit, rand + 1));
		digitActive = 0;
		//SetDigitActive();
		cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, 4, 0);
		rand = Random.Range (0, 9);
		if (rand == 0)
		{
			oneNum++;
		}
		digit = Instantiate (digits[rand], Parent.transform.position + new Vector3 (3.5f, 0f, 0f), digits[rand].transform.rotation, Parent.transform);
		cDigits.Add (new CDigit (digit, rand + 1));

		rand = Random.Range (0, 9);
		if (rand == 0)
		{
			oneNum++;
		}
		digit = Instantiate (digits[rand], Parent.transform.position + new Vector3 (2f, 0f, 0f), digits[rand].transform.rotation, Parent.transform);
		cDigits.Add (new CDigit (digit, rand + 1));

		StartCoroutine (Spawn_Digit ());

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			SetDigitActive ();
			//DeleteDigit(digitActive);
		}
	}

	IEnumerator Spawn_Digit()
	{
		while (!isGameOver)
		{
			yield return new WaitForSeconds (timeForWait);
			lastFrontier = EnemyManager.Instance.spawnBigDigits;                         //!!! Тут заменить на метод получение переменной "последний рубеж"
			if (cDigits.Count < 5)
			{
				int i = 0;
				int rand = 0;

				int mind = 0;
				int maxd = 9;

				if (lastFrontier)
				{
					maxLevelDigit = EnemyManager.Instance.GetMaxRank () - 1;      //n-1                                //!!! Тут заменить на метод получение переменной "максимальный уровень врага"
					mind = Mathf.Max (0, maxLevelDigit - 2);
					maxd = Mathf.Min (9, maxLevelDigit + 3);
					if (oneNum < minOneNumCount)
					{
						mind = 0;
						maxd = 0;
					}
				}

				if (oneNum >= maxOneNumCount)
					mind = 1;

				rand = Random.Range (mind, maxd);

				if (rand == 0)
				{
					oneNum++;
				}

				// yield return new WaitForSeconds(timeForWait);
				digit = Instantiate (digits[rand], Parent.transform.position, digits[rand].transform.rotation, Parent.transform);
				cDigits.Add (new CDigit (digit, rand + 1));
				if (digitActive == 0 && cDigits.Count == 1)
				{
					//cDigits[digitActive].gameObjectDigit.gameObject.GetComponent<SpriteRenderer>().sprite = spriteActiveDigit[cDigits[digitActive]._digit - 1];
					cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, 4, 0);
				}
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
			Destroy (cDigits[n].gameObjectDigit);
			cDigits.RemoveAt (n);
			if (cDigits.Count > 0)
			{
				//cDigits[digitActive].gameObjectDigit.gameObject.GetComponent<SpriteRenderer>().sprite = spriteActiveDigit[cDigits[digitActive]._digit - 1];
				cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, 4, 0);
			}
		}
	}
	public void SetDigitActive()
	{
		if (cDigits.Count > 0)
		{
			if (digitActive == cDigits.Count - 1)
			{
				//cDigits[digitActive].gameObjectDigit.gameObject.GetComponent<SpriteRenderer>().sprite = spriteDigit[cDigits[digitActive]._digit-1];
				cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, -4, 0);
				digitActive = 0;
				cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, 4, 0);
				//cDigits[digitActive].gameObjectDigit.gameObject.GetComponent<SpriteRenderer>().sprite = spriteActiveDigit[cDigits[digitActive]._digit-1];
			}
			else
			{
				//cDigits[digitActive].gameObjectDigit.gameObject.GetComponent<SpriteRenderer>().sprite = spriteDigit[cDigits[digitActive]._digit-1];
				cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, -4, 0);
				digitActive++;
				cDigits[digitActive].gameObjectDigit.transform.localScale += new Vector3 (0, 4, 0);
				//cDigits[digitActive].gameObjectDigit.gameObject.GetComponent<SpriteRenderer>().sprite = spriteActiveDigit[cDigits[digitActive]._digit-1];

			}
		}
	}
	public int GetDigitActive()
	{
		return cDigits[digitActive]._digit - 1;
	}
	public Sprite GetSpriteActiveDigit()
	{
		return spriteDigit[GetDigitActive ()];
	}

	public int GetCDigitLength()
	{
		return cDigits.Count;
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