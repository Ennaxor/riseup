using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class DestTerrainTile : TerrainTile {

#if UNITY_EDITOR
    [MenuItem("Assets/Create/DestroyedTile",false,1)]
        private static void CreateDestrTile()
    {
        DestTerrainTile myAT = new DestTerrainTile();
        Sprite[] myTextures = myAT.GetSprite("DestroyedRocks"); 
        myAT.InitiateSlots(myTextures);
        string fName = "DestroyedTile";
        myAT.name = fName + ".asset";
        AssetDatabase.CreateAsset(myAT, "Assets/Tiles/" + myAT.name + "");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        //Has to be placed before Create of Asset data will be lost after saving
    }
#endif


    //Heritage from TerrainTile could do this with  GetTileData too?
    public override Sprite[] GetSprite(string textName)
    {
        Sprite[] myTextures= base.GetSprite(textName);
        return myTextures;
    }






}
