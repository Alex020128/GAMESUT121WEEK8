using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystemLeftDoor : MonoBehaviour {

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;

    [Space]
    public List<RoomGridCell> gridCellList = new List<RoomGridCell>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public float cellSize = 1.0f;

    public Transform startLocation;

    // Start is called before the first frame update
    void Start() {
        
        random = new System.Random(PathSystemUpDoor.seed2);


        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new RoomGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++)
        {

            int n = random.Next(90);

            if (n.IsBetween(0, 29))
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
                gridCellList.Add(new RoomGridCell(currentPosition));
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else if (n.IsBetween(30, 59))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new RoomGridCell(currentPosition));

        }

    }

    private void OnDrawGizmos() {
        for (int i = 0; i < gridCellList.Count; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
