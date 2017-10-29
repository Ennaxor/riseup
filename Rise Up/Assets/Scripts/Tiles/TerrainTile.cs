using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

using System.Linq;

public abstract class TerrainTile : TileBase {


    public virtual Sprite[] GetSprite(string textureName)
    {
        Sprite[] myTextures = Resources.LoadAll<Sprite>("Textures/"+textureName) ;
        return myTextures;
    }

    public void UpdateDB(Object myAT , string name)
    {
        AssetDatabase.CreateAsset(myAT, "Assets/Tiles/" + name + "");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
	    
    public SpriteSlot[] spriteSlots;


    //...
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		Transform root = tilemap.GetComponent<Tilemap>().transform.parent;
		Tilemap lava = null;


        //se comprueba si top down left and right son del mismo tipo
        //Index created
        //Probably so that no exception is produced
        //Acid Tile has less than WallTile
        if (spriteSlots.Length < 9) return;
		int mask = (HasNeighbour(position + Vector3Int.up, tilemap) ? 1 : 0)
				+ (HasNeighbour(position + Vector3Int.right, tilemap) ? 2 : 0)
				+ (HasNeighbour(position + Vector3Int.down, tilemap) ? 4 : 0)
				+ (HasNeighbour(position + Vector3Int.left, tilemap) ? 8 : 0);
		
        //TILE STANDARD WHEN NO DIRT AROUND
        SpriteSlot slot = spriteSlots[0];
        	switch (mask)
        	{
            //que posicion de las ocho fichas pertence a que suma?

                //if same tile up
        		case  1: slot = spriteSlots[13]; break;
				//if same tile right
				case  2: slot = spriteSlots[1]; break;
				//if same tile up and right
				case  3: slot = spriteSlots[10]; break;
				//if same tile down
        		case  4: slot = spriteSlots[12]; break;
				//if same tile up and down
        		case  5: slot = spriteSlots[5]; break;
				//if same tile down and right
        		case  6: slot = spriteSlots[8]; break;
				//if same tile down up and right
        		case  7: slot = spriteSlots[6]; break;
				//if same tile left
				case  8: slot = spriteSlots[2]; break;
				//if same tile up and left
        		case  9: slot = spriteSlots[11]; break;
            	//if same tile right and left
                case 10:  slot = spriteSlots[14]; break;
				//if same tile left right and up
                case 11:  slot = spriteSlots[4]; break;
				//if same tile left and down
        		case 12: slot = spriteSlots[9]; break;
				//if same tile left down and up
        		case 13: slot = spriteSlots[7]; break;
				//if same tile left down and right
        		case 14: slot = spriteSlots[3]; break;
                //if same tile all - RANDOM betweeen 3
				case 15:							
						int total = spriteSlots [15].sprites.Sum (x => x.probability);

						int[] indices = new int[total];
						int spriteIndex = 0;	
						int indiceIndex = 0;
						foreach (var s in spriteSlots [15].sprites) {
							//indiceIndex
							for (int index = 0; index < s.probability; index++)
								indices [indiceIndex++] = spriteIndex;
							//spriteIndex += s.probability;
							spriteIndex++;
						}
						int random = Mathf.FloatToHalf (Random.value * total);
						int finalIndex = indices [Mathf.Clamp (random % total, 0, total - 1)];
									//spriteAux = spriteSlots[15].sprites[Mathf.Clamp(finalIndex, 0, spritesRandom.Length - 1)];
						spriteSlots [15].sprites[0].sprite = spriteSlots [15].sprites [Mathf.Clamp (finalIndex, 0, spriteSlots [15].sprites.Count-1)].sprite; 
						slot = spriteSlots [15];
						break;
        	}


        //Slot kann bis zu vier Sprites vorhalten für Random Auswahl
        tileData.sprite = slot.sprites[0].sprite;
        tileData.flags = TileFlags.LockAll;
        tileData.colliderType = mask != 15 ? Tile.ColliderType.Grid : Tile.ColliderType.None;
	}




    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
        foreach (var p in new BoundsInt(-1,-1, 0, 3, 3, 1).allPositionsWithin)
		{
			tilemap.RefreshTile(position + p);	
		}
	}



    public Sprite[] LoadTexture()
    {
        Sprite[] myTextures = Resources.LoadAll<Sprite>("LavaWalls") ;
        //Das hat funktioniert
        return myTextures;

    }



    public bool HasNeighbour(Vector3Int position, ITilemap tilemap)
    {
        TileBase tile = tilemap.GetTile(position);
        //tiledata check must be same kind of tile type
         
        //Important to check name property because we want tiles to change form depending on same name
        return (tile != null && tile.name== this.name );
    }


    public virtual void InitiateSlots(Sprite[] mySprite)
    {
        spriteSlots = new SpriteSlot[mySprite.Length];
        for (int i = 0; i < mySprite.Length; i++) {
	        SpriteSlot mySpSlot = new SpriteSlot(mySprite[i]);
	        spriteSlots[i] = mySpSlot;
        }
    }


    [System.Serializable]
    public class SpriteSlot {
        [SerializeField]
		public List<SpriteSlotItem> sprites;


        public SpriteSlot(Sprite spSprite)
        {
            sprites = new List<SpriteSlotItem>();
            sprites.Add(new SpriteSlotItem());
            sprites[0].sprite = spSprite;
        }


    }

	[System.Serializable]
	public class SpriteSlotItem
	{
        [SerializeField]
		public Sprite sprite;
        [SerializeField]
		public int probability = 1;
	}

}
