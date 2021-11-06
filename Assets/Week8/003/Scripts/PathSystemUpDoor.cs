using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystemUpDoor : MonoBehaviour
{
    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;

    [Space]
    public List<RoomGridCell> gridCellList = new List<RoomGridCell>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public float cellSize = 1.0f;
    public Transform startLocation;

    [Space]
    public static System.Random r;
    public static int seed1=0, seed2=0, seed3=0, seed4=0;
    // Start is called before the first frame update
    void Start()
    {
        if (seedType == SeedType.RANDOM)
        {
            r = new System.Random();
             seed1 = r.Next(-1000000000, 1000000000);
             seed2 = r.Next(-1000000000, 1000000000);
             seed3 = r.Next(-1000000000, 1000000000);
             seed4 = r.Next(-1000000000, 1000000000);
        }
        else if (seedType == SeedType.CUSTOM)
        {
            r = new System.Random(seed);
             seed1 = r.Next(-1000000000, 1000000000);
             seed2 = r.Next(-1000000000, 1000000000);
             seed3 = r.Next(-1000000000, 1000000000);
             seed4 = r.Next(-1000000000, 1000000000);
        }

        random = new System.Random(seed1);
        Debug.Log(seed1 + " " + seed2 + " " + seed3 + " " + seed4);


        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new RoomGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++)
        {

            int n = random.Next(90);

            if (n.IsBetween(0, 29))
            {
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
                gridCellList.Add(new RoomGridCell(currentPosition));
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
            }
            else if (n.IsBetween(30, 59))
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
                gridCellList.Add(new RoomGridCell(currentPosition));
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }

            gridCellList.Add(new RoomGridCell(currentPosition));

        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < gridCellList.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

