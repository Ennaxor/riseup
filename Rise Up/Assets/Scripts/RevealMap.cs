using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class RevealMap : MonoBehaviour  {

    //this is needed so other objects in the game can access the methods of this class
    public static RevealMap instance = null;

    public string k_GridName="Grid";
	public string k_RevealLayerName="GroundMap";
    public string k_PlaceMarkMap="MarkMap";
    public string k_PlaceObjMap = "GrassMap";
    //Tilemap for burning Ground
    Tilemap k_Tmap2;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }


    public void RevealTiles(Vector3 objectCoord,TileBase markTile, Transform bomb)
    {
       Grid mygrid=GetRootGrid(false);
       GridLayout mlOut = mygrid.GetComponent<GridLayout>(); 
       Vector3Int GridCoord = mlOut.LocalToCell(objectCoord);
    
       Tilemap k_Tmap = GetTileMap(k_RevealLayerName);
        k_Tmap2 = GetTileMap(k_PlaceMarkMap);
        k_Tmap2.SetTile(GridCoord, markTile);
        DeleteTiles(GridCoord, k_Tmap);
      }


    public void DeleteTiles(Vector3Int position, Tilemap tilemap) {
        //starttingpoint of explosion from current pos and size in three directions, no direction must be 0
        foreach (var p in new BoundsInt(-1, -1, 0, 3, 3, 1).allPositionsWithin) {
            tilemap.SetTile(position + p,null);
        }

    }


    //To access the tilemap wall
	public Tilemap GetTileMap(string tMpaName)
	{
		GameObject go = GameObject.Find(tMpaName);
		return go != null ? go.GetComponent<Tilemap>() : null;
	}


    public Grid GetRootGrid(bool autoCreate)
    {
        Grid result = null;


        if (result == null)
        {
            GameObject gridGameObject = GameObject.Find(k_GridName);
            if (gridGameObject != null && gridGameObject.GetComponent<Grid>() != null)
            {
                result = gridGameObject.GetComponent<Grid>();
            }
            else if (autoCreate)
            {
                gridGameObject = new GameObject(k_GridName);
                result = gridGameObject.AddComponent<Grid>();
            }

        }
            return result;
    }

	
}
