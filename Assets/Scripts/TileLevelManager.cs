using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileLevelManager : MonoBehaviour
{
    public List<ExtraGridInfo> gridList;

    public int gridSizeX = 10;
    public int gridSizeY = 10;

    void Start()
    {
        switchToPlayScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tileGrid.updateTileGrid();
            organizeTileGrid();
            switchToPlayScreen();
        }
    }

    void organizeTileGrid()
    {
        List<GameObject> newTileList = new List<GameObject>();

        //start at the bottom left and work up to the top right
        for (int y = 0; y < tileGrid.numCols; y++)
        {
            for (int x = 0; x < tileGrid.numRows; x++)
            {
                Vector2 pointToTest = new Vector2(
                    tileGrid.tileSize * x - tileGrid.tileSize / 2 + tileGrid.transform.position.x, 
                    tileGrid.tileSize * y - tileGrid.tileSize / 2 + tileGrid.transform.position.y);

                foreach (GameObject tile in tileGrid.tileList)
                {
                    if (tile.GetComponent<BoxCollider2D>().OverlapPoint(pointToTest))
                    {
                        newTileList.Add(tile);
                        break;
                    }
                }
            }
        }

        tileGrid.tileList = newTileList;
    }
}
