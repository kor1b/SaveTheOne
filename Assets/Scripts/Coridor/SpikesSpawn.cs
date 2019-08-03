using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesSpawn : MonoBehaviour
{
    [SerializeField] [Tooltip("Тайл шипов")]
    private TileBase tile;
    private Tilemap tilemap;
    [SerializeField] [Tooltip("Доступные для спавна на них линии")]
    private Line[] lines;
    [SerializeField] [Tooltip("Диапазон кол-ва шипов")]
    private Vector2 range;
    
    private List<Vector3Int> avalibleCoord = new List<Vector3Int>(); //cписок доступных 
    private List<Vector3Int> generated = new List<Vector3Int>(); //список точек, где создались шипы
    
    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnEnable()
    {
        FindAwalibleCoordinates();
        GenerateSpikes();
    }

    private void FindAwalibleCoordinates() //вычисляет доступные для спавна координаты
    {
        for (int i = 0; i < lines.Length; i++)
        {
            int x1 = (int) lines[i].points[0].position.x;
            int y1 = (int) lines[i].points[0].position.y;
            if (x1 == (int)lines[i].points[1].position.x) //вертикальная линия
            {
                int length = y1 - (int)lines[i].points[1].position.y;
                bool upsideDown = false;
                if (length < 0)
                {
                    upsideDown = true;
                    length *= -1;
                }
                for (int j = 0; j < length; j++)
                {
                   if (upsideDown)
                        avalibleCoord.Add(new Vector3Int(x1, y1 + j, 0));
                   else
                        avalibleCoord.Add(new Vector3Int(x1, y1 - j, 0));
                }
            }
            else //горизонтальная линия
            {
                int length = x1 - (int) lines[i].points[1].position.x;
                bool upsideDown = false;
                if (length < 0)
                {
                    upsideDown = true;
                    length *= -1;
                }
                for (int j = 0; j < length; j++)
                {
                    if(upsideDown)
                        avalibleCoord.Add(new Vector3Int(x1 + j, y1, 0));
                    else
                        avalibleCoord.Add(new Vector3Int(x1 - j, y1, 0));
                }
            }
        }
    }

    private void GenerateSpikes() //выбирает координаты из доступных и генерирует в них шипы
    {
        int count = (int) UnityEngine.Random.Range(range.x, range.y); //кол-во шипов, которые мы сгенерируем
        for (int i = 0; i < count; i++)
        {
            int index = UnityEngine.Random.Range(0, avalibleCoord.Count); //выбор координаты
            tilemap.SetTile(avalibleCoord[index], tile); //установка шипов
            generated.Add(avalibleCoord[index]); //добавить в список установленных
            avalibleCoord.RemoveAt(index); //удалить из списка доступных координат, чтобы избежать повторов
        }
    }

    private void OnDisable() //убирает все созданные шипы
    {
        for (int i = 0; i < generated.Count; i++)
        {
            tilemap.SetTile(generated[i], null);
        }
        generated.Clear();
    }
}

[Serializable]
class Line
{
    [SerializeField]
    public Transform[] points; //начальная и конечная точки
}

