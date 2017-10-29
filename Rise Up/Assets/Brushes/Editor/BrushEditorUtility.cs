using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
public class BrushEditorUtility 
{
	const string k_CameraName = "Camera";

	public static void UnpreparedSceneInspector()
	{
		GUILayout.Space(5f);
		GUILayout.Label("This scene is not yet ready for level editing.");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Initialize Scene"))
		{
			PrepareScene();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}

	public static bool SceneIsPrepared()
	{
		bool prepared = false;
		prepared = GameObject.Find(k_CameraName) != null;
		prepared &= BrushUtility.GetRootGrid(false);
		return prepared;
	}
	
	public static void PrepareScene()
	{
		GameObject rig = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Camera.prefab");
		GameObject player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
		GameObject gManag = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/GameManager.prefab");
		GameObject ground = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tilemaps/GroundMap.prefab");
		GameObject rock = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tilemaps/RockMap.prefab");
		GameObject dirt = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Tilemaps/DirtMap.prefab");

		if (rig != null && player != null && ground != null && rock != null && dirt != null && gManag != null )
		{
			RenderSettings.ambientLight = Color.white;
			foreach (var cam in Object.FindObjectsOfType<Camera>())
			{
				Object.DestroyImmediate(cam.gameObject, false);
			}
			Grid grid = BrushUtility.GetRootGrid(true);

			PrefabUtility.InstantiatePrefab(rig);



			GameObject groundGo = PrefabUtility.InstantiatePrefab(ground) as GameObject;
			PrefabUtility.DisconnectPrefabInstance(groundGo);
			groundGo.transform.SetParent(grid.transform);

			GameObject gManagGo = PrefabUtility.InstantiatePrefab(gManag) as GameObject;
			PrefabUtility.DisconnectPrefabInstance(gManagGo);
			gManagGo.transform.SetParent(grid.transform);

			GameObject rockGo = PrefabUtility.InstantiatePrefab(rock) as GameObject;
			PrefabUtility.DisconnectPrefabInstance(rockGo);
			rockGo.transform.SetParent(grid.transform);


			GameObject dirtGo = PrefabUtility.InstantiatePrefab(dirt) as GameObject;
			PrefabUtility.DisconnectPrefabInstance(dirtGo);
			dirtGo.transform.SetParent(grid.transform);

			GameObject playerGo = PrefabUtility.InstantiatePrefab(player) as GameObject;
			PrefabUtility.DisconnectPrefabInstance(playerGo);
			playerGo.transform.SetParent(grid.transform);


		}
		else
		{
			Debug.LogWarning("Some prefabs for initializing the scene are missing.");
		}
	}

	public static void AutoSelectGrid()
	{
		if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponentInParent<Grid>() == null)
		{
			Grid grid = BrushUtility.GetRootGrid(false);
			if(grid)
				Selection.activeTransform = grid.transform;
		}
	}

	public static void AutoSelectLayer(string name)
	{
		Transform transform = Selection.activeTransform;
		if (transform != null)
		{
			while (transform.parent != null)
			{
				if (transform.name == name)
				{
					return;
				}
				transform = transform.parent;
			}
		}

		AutoSelectGrid();
	}
}
#endif
