using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileLevelManager : MonoBehaviour
{
    public GameObject player;
    public List<ExtraGridInfo> gridList;

    public int gridSizeX = 10;
    public int gridSizeY = 10;

    public int tilesPerRow = 3;
    public int tilesPerCol = 3;

    public bool isPlaying = true;

    public ExtraGridInfo emptyTileObject;
    public ExtraGridInfo tileWithPlayerInIt;

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

                    if (currentTile != tileWithPlayerInIt)
                    {
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
            }

            if (!foundSlidingTile)
                areAnyTilesSliding = false;
        }
    }

    public void switchToPlaying()
    {
        isPlaying = true;
    }

    public void switchToNotPlaying()
    {
        isPlaying = false;
    }

    public void setTileWithPlayerInIt(ExtraGridInfo tile)
    {
        tileWithPlayerInIt = tile;
    }
}
