using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


[CustomGridBrush(false, true, false, "DirtBrush")]
public class DirtBrush : GridBrushBase {

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/DirtBrush",false,0)]
	//This Function is called when you click the menu entry
	private static void CreateAcidBrush()
	{
		string fileName = "DirtBrush";
		DirtBrush mytb = new DirtBrush();
		mytb.name = fileName + ".asset";
		AssetDatabase.CreateAsset(mytb, "Assets/Brushes/" +mytb.name + "");
	}
	#endif 

	public const string k_DirtLayer = "GroundMap";
	public const string k_GrassLayer = "GrassMap";
	public TileBase m_Dirt;
	public TileBase m_Grass;



	//...
	public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
	{
		//		GridInformation info = BrushUtility.GetRootGridInformation(true);
		Tilemap dirt = GetDirt(k_DirtLayer);

		if (dirt != null)
		{
			PaintInternal(position, dirt);
		}
	}

	private void PaintInternal(Vector3Int position, Tilemap tmap_over)
	{
		tmap_over.SetTile(position, m_Dirt);
	}

	public static Tilemap GetDirt(string Layername)
	{
		GameObject go = GameObject.Find(Layername);
		return go != null ? go.GetComponent<Tilemap>() : null;
	}

	//YT
	public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
	{
		Tilemap dirt = GetDirt(k_DirtLayer);
		Tilemap destr_dirt = GetDirt(k_GrassLayer);
		if (dirt != null )
		{
			EraseInternal(position, dirt, destr_dirt);
		}
	}

	private void EraseInternal(Vector3Int position, Tilemap dirt, Tilemap destr_dirt)
	{
		dirt.SetTile(position, null);
		destr_dirt.SetTile(position, m_Grass);
	}

}
