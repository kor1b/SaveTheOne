using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesSpawn : MonoBehaviour
{
    public LayerMask wallMask;
    [Header("Spike tiles")]
    [SerializeField]
    private TileBase tileLeft;
    [SerializeField]
    private TileBase tileRight;
    [SerializeField]
    private TileBase tileUp;
    [SerializeField]
    private TileBase tileDown;

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
        //GenerateAll();
    }

    private void FindAwalibleCoordinates() //вычисляет доступные для спавна координаты
    {
        for (int i = 0; i < lines.Length; i++)
        {
            int x1 = (int) lines[i].points[0].localPosition.x;
            int y1 = (int) lines[i].points[0].localPosition.y;
            if (x1 == (int)lines[i].points[1].localPosition.x) //вертикальная линия
            {
                int length = y1 - (int)lines[i].points[1].localPosition.y;
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
                int length = x1 - (int) lines[i].points[1].localPosition.x;
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
            TileBase tile =  FindDirection(avalibleCoord[index]);
            tilemap.SetTile(avalibleCoord[index], tile); //установка шипов

            //Debug.Log("Spike generated " + avalibleCoord[index]);

            generated.Add(avalibleCoord[index]); //добавить в список установленных
            avalibleCoord.RemoveAt(index); //удалить из списка доступных координат, чтобы избежать повторов
        }
    }
    
    private void OnDisable() //убирает все созданные шипы
    {
        for (int i = 0; i < generated.Count; i++)
        {
            tilemap.SetTile(generated[i], null);
           // Debug.Log("Spike removed " + generated[i]);
        }
        generated.Clear();
        avalibleCoord.Clear();
    }

    private TileBase FindDirection(Vector3 coordinate)
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(coordinate, Vector3.down, 1f, wallMask);
        if (!hit)
        {
            hit = Physics2D.Raycast(coordinate, Vector3.right, 1f, wallMask);
            if (hit)
            {
                Debug.Log("Right");
                return tileRight;
            }
            else
            {
                Debug.Log("Down");
                return tileDown;
            }
        }
        else
        {
            hit = Physics2D.Raycast(coordinate + Vector3.up, Vector3.left, 1f, wallMask);
            if (!hit)
            {
                Debug.Log("Up");
                return tileUp;
            }
            hit = Physics2D.Raycast(coordinate + Vector3.right, Vector3.left, 1.5f, wallMask);
            if (hit)
            {
                Debug.Log("Left");
                return tileLeft;
            }
            
        }
      /*  hit = Physics2D.Raycast(coordinate, Vector3.right, 1f, wallMask);
        if (!hit)
        {
            Debug.Log("Left");
            return tileLeft;
        }

        hit = Physics2D.Raycast(coordinate, Vector3.up, 1f, wallMask);
        if (!hit)
        {
            Debug.Log("Up");
            return tileUp;
        }*/

      /*  hit = Physics2D.Raycast(coordinate, Vector3.right, 1f, wallMask);
        if (!hit)
        {
            Debug.Log("Right");
            return tileRight;
        }*/


      /*  hit = Physics2D.Raycast(coordinate, Vector3.right, 1f, wallMask);
        if (hit)
        {
            Debug.Log("Right1");

           // Vector3 newCoord;
            //newCoord = coordinate + Vector3.

            return tileLeft;
            /*hit = Physics2D.Raycast(coordinate, Vector3.left, 1f, wallMask);
            if (hit)
            {
                Debug.Log("Left");
                return tileRight;
            }*/
     // }
    
        
        /*hit = Physics2D.Raycast(coordinate, Vector2.up, 1f, wallMask);
        if (hit)
        {
            Debug.Log("Up");
            return tileUp;
        }*/
        /*hit = Physics2D.Raycast(coordinate, Vector2.down, 1f);
        if (hit.collider != null)
        {
            Debug.Log("Down");
            return tileDown;
        }*/

        Debug.Log("No wall!");
        Debug.Log(coordinate);
        return null;
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < avalibleCoord.Count; i++)
        {
            Gizmos.DrawWireSphere(avalibleCoord[i], 0.5f);
           // Gizmos.DrawLine(avalibleCoord[i], );
        }
    }*/

    void GenerateAll()
    {
        foreach (Vector3Int index in avalibleCoord) {
            TileBase tile = FindDirection(index);
            tilemap.SetTile(index, tileLeft);
        }
    }
}
[Serializable]
class Line
{
    [SerializeField]
    public Transform[] points; //начальная и конечная точки
}

