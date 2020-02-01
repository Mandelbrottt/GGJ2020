using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public GameObject tileObject;

    public float tileSize = 3;

    public int numRows = 3;
    public int numCols = 3;

    private List<GameObject> tileList = new List<GameObject>();
    private GameObject emptyTileObject;

    private bool areAnyTilesSliding = false;

    void initGrid()
    {
        //start at the bottom left and work up to the top right
        for (int y = 0; y < numCols; y++)
        {
            for (int x = 0; x < numRows; x++)
            {
                GameObject newTile = Instantiate(tileObject);

                Vector3 newPosition = new Vector3(tileSize * x - 10.0f, tileSize * y - 10.0f, 1.0f);
                newTile.transform.position = newPosition;

                if (y == 2 && x == 1)
                {
                    newTile.GetComponent<TileBehaviour>().isEmptyTile = true;
                    emptyTileObject = newTile;
                    newTile.SetActive(false);
                }

                tileList.Add(newTile);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initGrid();
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
}
