using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;


[CustomEditor(typeof(RockBrush))]
public class LavaBrushEditor : GridBrushEditorBase {


	public override void OnPaintInspectorGUI()
	{

        //Button anzeigen, dass die Szene erstellt werden soll
        if (BrushEditorUtility.SceneIsPrepared())
        {
            GUILayout.Label("Use this custom Brush to paint some Rocks on the map!");

        }
        else
        {
            BrushEditorUtility.UnpreparedSceneInspector();
        }
    }


}
