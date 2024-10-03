using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class mazeGenerator : MonoBehaviour
{
    [SerializeField]
    private mazeCell mazeCellPrefab;

    [SerializeField]
    private int mazeWidth;

    [SerializeField]
    private int mazeDepth;

    private mazeCell[,] mazeGrid;

    public mazeCell lastCell;

    public GameObject lastCellPrefab;


    // Start is called before the first frame update
    void Start()
    {
        mazeGrid = new mazeCell[mazeWidth, mazeDepth];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeDepth; z++)
            {
                mazeGrid[x,z] = Instantiate(mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        GenerateMaze(null, mazeGrid[0,0]);

        // last cell entry 
        int x1 = (int)lastCell.transform.position.x;
        int z1 = (int)lastCell.transform.position.z;
        Instantiate(lastCellPrefab, new Vector3(x1, 1.33f, z1), Quaternion.identity);
    }

    private void GenerateMaze(mazeCell previousCell, mazeCell currentCell)
    {

        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        mazeCell nextCell ;

        do 
        {

        nextCell = GetNextUnvisitedCell(currentCell);

        if (nextCell != null)
        {
            GenerateMaze(currentCell, nextCell);

            lastCell = currentCell;
        }
        } while (nextCell != null);


    }

    private mazeCell GetNextUnvisitedCell(mazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1,10)).FirstOrDefault();
    }

    private IEnumerable<mazeCell> GetUnvisitedCells(mazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x + 1, z];

            if (cellToRight.isVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0 )
        {
            var cellToLeft = mazeGrid[x - 1, z];

            if (cellToLeft.isVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x,z + 1];

            if (cellToFront.isVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, z - 1];

            if (cellToBack.isVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(mazeCell previousCell, mazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
