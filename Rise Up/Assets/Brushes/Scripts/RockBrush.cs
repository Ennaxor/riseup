using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


[CustomGridBrush(false, true, false, "RockBrush")]
public class RockBrush : GridBrushBase {

#if UNITY_EDITOR
    [MenuItem("Assets/Create/RockBrush",false,0)]
        //This Function is called when you click the menu entry
        private static void CreateAcidBrush()
        {
        string fileName = "RockBrush";
        RockBrush mytb = new RockBrush();
        mytb.name = fileName + ".asset";
        AssetDatabase.CreateAsset(mytb, "Assets/Brushes/" +mytb.name + "");
    }
#endif 

	public const string k_RockLayer = "RockMap";
	public const string k_RockDestroyedLayer = "GroundMap";
	public TileBase m_Rock;
	public TileBase m_Destroyed;
	public TileBase m_Ground;





    //Paint internal macht wahrscheinlich selber noch kein Tile an die gewünschte Position
    public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
    {
//		GridInformation info = BrushUtility.GetRootGridInformation(true);
        Tilemap rock = GetRock(k_RockLayer);
        Tilemap destr_rock = GetRock(k_RockDestroyedLayer);

		if (rock != null)
		{
            			PaintInternal(position, rock, destr_rock);
		}
    }

    private void PaintInternal(Vector3Int position, Tilemap tmap_over, Tilemap tmap_under)
    {
        tmap_over.SetTile(position, m_Rock);
        tmap_under.SetTile(position, m_Destroyed);
    }

	public static Tilemap GetRock(string Layername)
	{
		GameObject go = GameObject.Find(Layername);
		return go != null ? go.GetComponent<Tilemap>() : null;
    }

    //YT
	public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
	{
        Tilemap rock = GetRock(k_RockLayer);
        Tilemap destr_rock = GetRock(k_RockDestroyedLayer);

		if (rock != null )
		{
			EraseInternal(position, rock, destr_rock);
		}
	}

	private void EraseInternal(Vector3Int position, Tilemap rock, Tilemap destr_rock)
	{
		rock.SetTile(position, null);
		destr_rock.SetTile(position, m_Ground);
	}

}
