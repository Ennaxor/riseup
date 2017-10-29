using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


[CustomGridBrush(false, true, false, "SandBrush")]
public class SandBrush : GridBrushBase {

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/SandBrush",false,0)]
	//This Function is called when you click the menu entry
	private static void CreateAcidBrush()
	{
		string fileName = "SandBrush";
		SandBrush mytb = new SandBrush();
		mytb.name = fileName + ".asset";
		AssetDatabase.CreateAsset(mytb, "Assets/Brushes/" +mytb.name + "");
	}
	#endif 

	/*public const string k_DirtLayer = "GroundMap";
	public const string k_GrassLayer = "GrassMap";
	public TileBase m_Dirt;
	public TileBase m_Grass;*/

	public const string k_SandLayer = "SandMap";
	//public const string k_GrassLayer = "GrassMap";
	public const string k_GroundLayer = "GroundMap";
	public TileBase m_Sand;
	//public TileBase m_Grass;
	public TileBase m_Ground;
	//...
	public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
	{
		//		GridInformation info = BrushUtility.GetRootGridInformation(true);
		Tilemap sand = GetTileTileMap(k_SandLayer);

		if (sand != null)
		{
			PaintInternal(position, sand);
		}
	}

	private void PaintInternal(Vector3Int position, Tilemap tmap_over)
	{
		tmap_over.SetTile(position, m_Sand);
	}

	public static Tilemap GetTileTileMap(string Layername)
	{
		GameObject go = GameObject.Find(Layername);
		return go != null ? go.GetComponent<Tilemap>() : null;
	}

	//YT
	public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
	{
		Tilemap dirt = GetTileTileMap(k_SandLayer);
		Tilemap ground = GetTileTileMap(k_GroundLayer);
		if (dirt != null )
		{
			EraseInternal(position, dirt, ground);
		}
	}

	private void EraseInternal(Vector3Int position, Tilemap dirt, Tilemap ground)
	{
		dirt.SetTile(position, null);
		ground.SetTile(position, m_Ground);
	}

}
