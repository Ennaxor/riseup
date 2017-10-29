using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RockTile : TerrainTile {

#if UNITY_EDITOR
    [MenuItem("Assets/Create/RockTile",false,1)]
        private static void CreateRockTile()
    {
        Sprite[] myTextures = InitiateSlots(); 

        if (myTextures != null)
        { 
//            Debug.Log(myTextures.GetType() + "Length: " + myTextures.Length );
 //           Debug.Log(myTextures[0].name);
            }
        else
        {
            Debug.Log("Texture not loaded");
        }

        string fName2 = "RockTile";
        RockTile myAT = new RockTile();

        myAT.name = fName2 + ".asset";
        //Has to be placed before Create of Asset data will be lost after saving
        myAT.InitiateSlots(myTextures);
        AssetDatabase.CreateAsset(myAT, "Assets/Tiles/" +myAT.name + "");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static Sprite[] InitiateSlots()
    {
        Sprite[] myTextures = Resources.LoadAll<Sprite>("Textures/Rocks") ;
        return myTextures;
    }
#endif


    //Heritage from TerrainTile could do this with  GetTileData too?
    public override Sprite[] GetSprite(string textName)
    {
        Sprite[] myTextures= base.GetSprite(textName);
        return myTextures;
    }


}

