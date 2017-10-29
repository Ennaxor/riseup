using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class BombPlacer : MonoBehaviour {

	protected virtual Vector3 offsetFromBottomLeft { get { return m_PrefabOffset; } }
	public virtual bool alwaysCreateOnPaint { get { return false; } }
	public GameObject m_Prefab;
    public string k_GroundLayer;
    public string k_DestroyLayer;
	public Vector3 m_PrefabOffset;
    Player heroInstance;
    UnityAction eventListener;
    AudioSource audioSource;
    AudioClip dropClip;
    BoxCollider2D bCol;
    Controller2D controller;

    private void Awake()
    {
        bCol = GetComponent<BoxCollider2D>();
//        Debug.Log("sprite center: " + bCol.size.x/2+ "," + bCol.size.y/2);
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<Controller2D>();

        dropClip = Resources.Load<AudioClip>("Audio/dropbomb");
        heroInstance = FindObjectOfType<Player>().GetComponent<Player>();
        //From Playerinstancke
        eventListener = new UnityAction(PlaceBomb);
        heroInstance.BombAction.AddListener(eventListener);
        
        

    }
    private void PlaceBomb()
    {
            if (audioSource != null)
            audioSource.Play();
            CreateObject();
    }


//    protected void CreateObject(GridLayout grid, Vector3Int position, GameObject prefab)
    protected void CreateObject()
	{


            Grid grid = BrushUtility.GetRootGrid(true);
			Transform destroyGo = GameObject.Find(k_DestroyLayer).transform;
            Tilemap destroy;
				destroy = destroyGo.GetComponent<Tilemap>();
        //Check on Tilemap and move Player is above is wall
        //Ref to groundmap needed and has wall
        //        Vector3Int position = Vector3Int.RoundToInt(new Vector3(bCol.transform.position.x+bCol.size.x/2,bCol.transform.position.y+bCol.size.y,bCol.transform.position.z));
        Vector3Int position = grid.WorldToCell(this.transform.position);




		int mask = (HasNeighbour(position + Vector3Int.up, destroy) ? 1 : 0)
				+ (HasNeighbour(position + Vector3Int.right, destroy) ? 2 : 0)
				+ (HasNeighbour(position + Vector3Int.down, destroy) ? 4 : 0)
				+ (HasNeighbour(position + Vector3Int.left, destroy) ? 8 : 0);

		if (m_Prefab.GetComponent<Bomb>() != null)
		{
//            Debug.Log("masek val: " + mask + " Vector checked: " + new Vector2 (position.x,position.y));
			GameObject newObj = BrushUtility.Instantiate(m_Prefab, transform.position + offsetFromBottomLeft, GetLayer());
		}
		else
		{
			Debug.LogError("Prefab " + m_Prefab.name + " doesn't contain component " + typeof(Bomb) + ", brush paint operation cancelled.");
		}

        switch (mask)
        {

            //Move Player
            //YT New
            //check only below
            case 4:  controller.Move(Vector3Int.up); break;
                //check if above and below
            case 5:  controller.Move(Vector3Int.right); break;
            case 6:  controller.Move(Vector3Int.up); break;
            case 12:  controller.Move(Vector3Int.up); break;
            case 14:  controller.Move(Vector3Int.up); break;
            //check only above
            //case 1:  this.transform.position = Vector3Int.RoundToInt(this.transform.position) + Vector3Int.down; break;
            case 1:  controller.Move(Vector3Int.down); break;
            case 9:  controller.Move(Vector3Int.down); break;
            case 11:  controller.Move(Vector3Int.down); break;
            case 10:  controller.Move(Vector3Int.up); break;
            case 3:  controller.Move(Vector3Int.down); break;
           //check only left
//            case 8:  this.transform.position = Vector3Int.RoundToInt(this.transform.position) + Vector3Int.right; break;
            case 8:  controller.Move(Vector3Int.right); break;
           //check only right
            //case 2:  this.transform.position = Vector3Int.RoundToInt(this.transform.position) + Vector3Int.left; break;
            case 2:  controller.Move(Vector3Int.left); break;
           //nothing around
            //case 0:  this.transform.position = Vector3Int.RoundToInt(this.transform.position) + Vector3Int.down; break;
            case 0:  controller.Move(Vector3Int.left); break;

        }

	}


	public Transform GetLayer()
	{
		Transform layer = BrushUtility.GetRootGrid(false).transform.Find(k_GroundLayer);
		if (layer == null)
		{
			GameObject newGameObject = new GameObject(k_GroundLayer);
			layer = newGameObject.transform;
			layer.SetParent(BrushUtility.GetRootGrid(false).transform);
		}
		return layer;
	}

    public bool HasNeighbour(Vector3Int position, Tilemap ground)
    {
        TileBase tile = ground.GetTile(position);
        return (tile != null );
    }

        
}