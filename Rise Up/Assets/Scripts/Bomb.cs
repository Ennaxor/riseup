using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour {
    Grid grid;
    Vector3 position;
    Vector3 childvelocity;
    BoxCollider2D bombCollider;
    AudioSource audioBomb;
    Rigidbody2D rgbody2D;
    public TileBase bombMark;
    private Animator animBomb;
    private SpriteRenderer spriteBomb;
    AudioClip explosionClip;
    AudioClip dropClip;


    private void Awake()
    {
            
    }
	// Use this for initialization
	void Start () {
        //As soon as Bomb is alive
        audioBomb = GetComponent<AudioSource>();
        audioBomb.Play();
        explosionClip = Resources.Load<AudioClip>("Audio/explosion");
        grid = BrushUtility.GetRootGrid(true);
        bombCollider = GetComponent<BoxCollider2D>();
        rgbody2D = GetComponent<Rigidbody2D>();
        spriteBomb = GetComponent<SpriteRenderer>();
	}

    //Is called from Animation
    //IEnumerator Explode() {
    public void Explode() {
        audioBomb.clip= explosionClip;
        audioBomb.Play();
        spriteBomb.sortingOrder = -100;
//        yield return new WaitForSeconds(0.2f);
        rgbody2D.velocity = Vector2.zero;
        //Position of the gameobject taken from grid
        bombCollider.enabled = false;
        position = this.transform.position;
        RevealMap.instance.RevealTiles(position, bombMark, this.transform);
    }

}
