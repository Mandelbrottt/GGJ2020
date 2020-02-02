using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileLevelManager : MonoBehaviour
{
    public List<ExtraGridInfo> gridList;

    public int gridSizeX = 10;
    public int gridSizeY = 10;

    public int tilesPerRow = 3;
    public int tilesPerCol = 3;

    public bool isPlaying = true;

    public ExtraGridInfo emptyTileObject;

    public bool areAnyTilesSliding = false;

    void Start()
    {

    }

    private void Update()
    {
        if (!isPlaying)
        {
            bool foundSlidingTile = false;

            int numTiles = gridList.Count;
            for (int i = 0; i < numTiles; i++)
            {
                ExtraGridInfo currentTile = gridList[i].GetComponent<ExtraGridInfo>();

                if (currentTile.isSliding)
                {
                    foundSlidingTile = true;
                    areAnyTilesSliding = true;
                }

                if (currentTile.wasClicked)
                {
                    currentTile.wasClicked = false;

                    //invalid if any tiles are already sliding or the tile isn't touching the empty tile
                    if (!areAnyTilesSliding
                        && (currentTile.transform.position - emptyTileObject.transform.position).magnitude == gridSizeX
                        && !currentTile.isEmptyTile)
                    {
                        currentTile.startPos = currentTile.transform.position;
                        currentTile.targetPos = emptyTileObject.transform.position;
                        currentTile.isSliding = true;

                        emptyTileObject.transform.position = currentTile.transform.position;
                    }
                }
            }

            if (!foundSlidingTile)
                areAnyTilesSliding = false;
        }
    }

    void organizeTileGrid()
    {
        //List<ExtraGridInfo> newGridList = new List<ExtraGridInfo>();

        ////start at the bottom left and work up to the top right
        //for (int y = 0; y < tilesPerCol; y++)
        //{
        //    for (int x = 0; x < tilesPerRow; x++)
        //    {
        //        Vector2 pointToTest = new Vector2(
        //            gridSizeX * x - gridSizeX / 2 - gridSizeX,
        //            gridSizeY * y - gridSizeY / 2 - gridSizeY * 2);

        //        foreach (ExtraGridInfo grid in gridList)
        //        {
        //            if (grid.GetComponent<BoxCollider2D>().OverlapPoint(pointToTest))
        //            {
        //                newGridList.Add(grid);
        //                break;
        //            }
        //        }
        //    }
        //}

        //gridList = newGridList;
    }

    void initTileGrid()
    {
        //int i = 0;
        ////start at the bottom left and work up to the top right
        //for (int y = 0; y < tilesPerCol; y++)
        //{
        //    for (int x = 0; x < tilesPerRow; x++)
        //    {
        //        GameObject newTile = Instantiate(tileObject);
        //        newTile.GetComponent<TileBehaviour>().tileID = i;
        //        newTile.transform.parent = this.transform;

        //        Vector3 newPosition = new Vector3(tileSize * x - tileSize, tileSize * y - tileSize, 1.0f);
        //        newTile.transform.position = newPosition;

        //        if (y == 2 && x == 1)
        //        {
        //            newTile.GetComponent<TileBehaviour>().isEmptyTile = true;
        //            emptyTileObject = newTile;
        //            newTile.SetActive(false);
        //        }

        //        tileList.Add(newTile);

        //        i++;
        //    }
        //}
    }
}
