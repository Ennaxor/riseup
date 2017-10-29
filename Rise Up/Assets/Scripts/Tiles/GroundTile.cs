using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundTile : RandomTile {

    [MenuItem("Assets/Create/GroundTile",false,1)]
        private static void CreateRandomTile()
    {
        string fName2 = "GroundTile";
        RandomTile myAT = new RandomTile();
        myAT.name = fName2;
        //Has to be placed before Create of Asset data will be lost after saving
        AssetDatabase.CreateAsset(myAT, "Assets/Tiles/" +fName2 + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
