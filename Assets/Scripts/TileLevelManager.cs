using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class RandomStuff
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

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
        initTileGrid();
    }

    private void Update()
    {
        if (!isPlaying)
        {
            checkForTileSliding();
        }
    }

    private void initTileGrid()
    {
        //RandomStuff.Shuffle(gridList);

        int i = 0;
        //start at the bottom left and work up to the top right
        for (int y = 0; y < tilesPerCol; y++)
        {
            for (int x = 0; x < tilesPerRow; x++)
            {
                gridList[i].transform.localPosition = new Vector3(gridSizeX * x, gridSizeY * y - gridSizeY * 2.0f, 1.0f);
                i++;
            }
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

    private void checkForTileSliding()
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
