using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class RoomGrid : MonoBehaviour
{

    public List<RoomGridCell> gridCellList = new List<RoomGridCell>();
    public List<RoomGridCell> ObstacleCellList = new List<RoomGridCell>();
    public List<RoomGridCell> resultCells = new List<RoomGridCell>();
    public List<RoomGridCell> pathCells = new List<RoomGridCell>();

    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1.0f;

    public GameObject rock;
    public GameObject mushroom;
    public GameObject path;
    public GameObject obstacle;
    public int rockSpawnCount = 10;
    public int mushroomSpawnCount = 10;

    public List<RoomGridCell> PathSystemLeft;
    public List<RoomGridCell> PathSystemUp;
    public List<RoomGridCell> PathSystemDown;
    public List<RoomGridCell> PathSystemRight;

    public GameObject[] rocks;
    public GameObject[] mushrooms;

    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        DrawGrid();
        DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        random = new System.Random((int)(DateTime.Now - epochStart).TotalSeconds);

        PathSystemLeft = GameObject.Find("PathSystemLeft").GetComponent<PathSystemLeftDoor>().gridCellList;
        PathSystemUp = GameObject.Find("PathSystemUp").GetComponent<PathSystemUpDoor>().gridCellList;
        PathSystemDown = GameObject.Find("PathSystemDown").GetComponent<PathSystemDownDoor>().gridCellList;
        PathSystemRight = GameObject.Find("PathSystemRight").GetComponent<PathSystemRightDoor>().gridCellList;

        resultCells.Clear();
        ObstacleCellList.Clear();
        pathCells.Clear();

        rocks = GameObject.FindGameObjectsWithTag("Rock");
        mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");

        foreach (GameObject rock in rocks)
        {
            Destroy(rock);
        }

        foreach (GameObject mushroom in mushrooms)
        {
            Destroy(mushroom);
        }

        pathCells.AddRange(PathSystemLeft);
        pathCells.AddRange(PathSystemUp);
        pathCells.AddRange(PathSystemDown);
        pathCells.AddRange(PathSystemRight);

        for (int i = 0; i < gridCellList.Count; i++)
        {
            ObstacleCellList.Add(gridCellList[i]);
            Instantiate(obstacle, gridCellList[i].location, Quaternion.identity);

        }

        foreach (RoomGridCell item1 in ObstacleCellList)
        {

            bool overlap = false;

            Debug.Log(item1.location);


            foreach (RoomGridCell item2 in pathCells)
            {

                Instantiate(path, pathCells[random.Next(pathCells.Count)].location, Quaternion.identity);

                Debug.Log(item2.location);

                if (item1.location == item2.location)
                {
                    overlap = true;
                }

            }

            if (!overlap)
            {
                resultCells.Add(item1);
            }

        }

        for (int i = 0; i < rockSpawnCount; i++)
        {
            Instantiate(rock, resultCells[random.Next(resultCells.Count)].location, Quaternion.identity);
        }

        for (int i = 0; i < mushroomSpawnCount; i++)
        {
            Instantiate(mushroom, resultCells[random.Next(resultCells.Count)].location, Quaternion.identity);
        }
    }

    void DrawGrid()
    {
        float startX = ((-columns / 2.0f) * cellSize) + (cellSize / 2.0f);
        float startY = ((-rows / 2.0f) * cellSize) + (cellSize / 2.0f);

        for (int i = 0; i < rows; i++)
        {
            Debug.Log($"Hey this is number #{i}");
            for (int j = 0; j < columns; j++)
            {
                gridCellList.Add(
                    new RoomGridCell(
                         startX + (j * cellSize),
                         startY + (i * cellSize)
                         )
                    );
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < gridCellList.Count; i++)
        {
            Gizmos.DrawWireCube(gridCellList[i].location, Vector2.one * cellSize);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Isaac");
        }
    }
}

[System.Serializable]
public class RoomGridCell
{

    public Vector2 location;

    public RoomGridCell() { }
    public RoomGridCell(Vector2 v)
    {
        location = new Vector2(v.x, v.y);
    }
    public RoomGridCell(float x, float y)
    {
        location = new Vector2(x, y);
    }

}