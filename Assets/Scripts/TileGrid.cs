using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileGrid : MonoBehaviour
{
    public GameObject tileObject;

    public float tileSize = 16;

    public int numRows = 4;
    public int numCols = 4;

    public List<GameObject> tileList = new List<GameObject>();
    public GameObject emptyTileObject;

    public bool areAnyTilesSliding = false;

    // Start is called before the first frame update
    void Start()
    {
        initTileGrid();
    }

    // Update is called once per frame
    void Update()
    {
        bool foundSlidingTile = false;

        int numTiles = tileList.Count;
        for (int i = 0; i < numTiles; i++)
        {
            TileBehaviour currentTile = tileList[i].GetComponent<TileBehaviour>();

            if (currentTile.isSliding)
            {
                foundSlidingTile   = true;
                areAnyTilesSliding = true;
            }

            if (currentTile.wasClicked)
            {
                currentTile.wasClicked = false;

                //invalid if any tiles are already sliding or the tile isn't touching the empty tile
                if (   !areAnyTilesSliding 
                    && (currentTile.transform.position - emptyTileObject.transform.position).magnitude == tileSize 
                    && !currentTile.isEmptyTile)
                {
                    currentTile.startPos  = currentTile.transform.position;
                    currentTile.targetPos = emptyTileObject.transform.position;
                    currentTile.isSliding = true;

                    emptyTileObject.transform.position = currentTile.transform.position;
                }
            }
        }

        if (!foundSlidingTile)
            areAnyTilesSliding = false;
    }

    void initTileGrid()
    {
        int i = 0;
        //start at the bottom left and work up to the top right
        for (int y = 0; y < numCols; y++)
        {
            for (int x = 0; x < numRows; x++)
            {
                GameObject newTile = Instantiate(tileObject);
                newTile.GetComponent<TileBehaviour>().tileID = i;
                newTile.transform.parent = this.transform;

                Vector3 newPosition = new Vector3(tileSize * x, tileSize * y, 1.0f);
                newTile.transform.position = newPosition;

                if (y == 2 && x == 1)
                {
                    newTile.GetComponent<TileBehaviour>().isEmptyTile = true;
                    emptyTileObject = newTile;
                    newTile.SetActive(false);
                }

                tileList.Add(newTile);

                i++;
            }
        }
    }

    public void updateTileGrid()
    {
        int i = 0;
        //start at the bottom left and work up to the top right
        for (int y = 0; y < numCols; y++)
        {
            for (int x = 0; x < numRows; x++)
            {
                tileList[i].transform.localPosition = new Vector3(tileSize * x - tileSize, tileSize * y - tileSize, 1.0f);

                i++;
            }
        }
    }
}
