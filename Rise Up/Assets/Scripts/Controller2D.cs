using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A raycast Controller handles Slopes and moving platforms far more consistantly
//better finetune control of player movement
//Class has no updatae method
[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {
    //Seb Lague

    public LayerMask collisionMask;
    public int horRayCount=4;
    public int vertRayCount=4;
    public float skinWidth = .015f;

    BoxCollider2D collider;
    RayCastOrigins rayCOrig;
    public CollisionInfo collinfo;

    //skinWidth used because can still fire rays while on the ground


    float horRaySpacing;
    float vertRaySpacing;
	// Use this for initialization

	void Start () {
        //struct that tells us, where our player collided
        //structs can not be instantiated
        collinfo.ResetColl();
        collider = GetComponent<BoxCollider2D>();
        //We need this only once
        CalcRaySpacing();
	}



    public bool Move(Vector3 velocity)
    {
        bool onPlatform=true;
        UpdateRaycastOrig();
        collinfo.ResetColl();

        //Direct change of the passed variable

        if (velocity.x != 0)
        HorizontalCollisions(ref velocity);
        if (velocity.y != 0)
        VerticalCollisions(ref velocity, ref onPlatform);


        transform.Translate(velocity);
        return onPlatform;
    }

    #region Collidions Detection
    void VerticalCollisions(ref Vector3 velocity, ref bool onP)
    {

        //positive if moving up, neg. if moving down
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y + skinWidth);
        //Debug Rays
        for (int i = 0; i < vertRayCount; i++)
        {
            //Depeding on moving up or down neg. or pos. sign
            //if evluation of term in first bracket is true
            Vector2 rayOrigin = (directionY == -1) ? rayCOrig.botLeft : rayCOrig.topLeft;
//        Debug.Log("rayOrig:  "+ directionY);

            if (i==0)
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            //rayc Orig is calc for each point where we will be after the PLANNED movement
            //starting from first calc point from method
            rayOrigin += Vector2.right * (vertRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            //How much to move from current pos. to point where intersection of ray with obstacle
            if (hit)
            {
//                Debug.Log("hit obj: " + hit.collider.name);
                rayLength = hit.distance;

                velocity.y = (hit.distance - skinWidth) * directionY;
                //if movement to right, velocity always has to be the the maximal movement of the 
                //rayOrigin, hit of first ray gives length to all following rays video e02.coll dect. 9:15
                collinfo.above = directionY == 1;
                collinfo.below = directionY == -1;
            }

            if (i!=0)
            {

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            }

        }
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {

        //positive if moving up, neg. if moving down
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x + skinWidth);
        //Debug Rays
        for (int i = 0; i < horRayCount; i++)
        {
            //Depeding on moving up or down neg. or pos. sign
            //if evluation of term in first bracket is true
            Vector2 rayOrigin = (directionX == -1) ? rayCOrig.botLeft : rayCOrig.botRight;
            //This var is calc differently to vert movement
            rayOrigin += Vector2.up * (horRaySpacing * i );
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            //How much to move from current pos. to point where intersection of ray with obstacle
            if (hit)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    //if movement to right, velocity always has to be the the maximal movement of the 
                    //rayOrigin, hit of first ray gives length to all following rays video e02.coll dect. 9:15
                    rayLength = hit.distance;
                    collinfo.left = directionX == -1;
                    collinfo.right = directionX == 1;
                }
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength , Color.blue);
        }

        for (int i = 0; i < horRayCount; i++)
        {
                Debug.DrawRay(rayCOrig.botRight + Vector2.up * horRaySpacing * i, Vector2.left * -2, Color.blue);
        }

    }
#endregion 

    #region Preparing Rays
    void UpdateRaycastOrig()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        rayCOrig.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCOrig.botRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCOrig.topRight = new Vector2(bounds.max.x, bounds.max.y);
        rayCOrig.botLeft = new Vector2(bounds.min.x, bounds.min.y);
    }

    void CalcRaySpacing() {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        //Assure, that we have at least two rays in each direction
        horRayCount = Mathf.Clamp(horRayCount, 2, int.MaxValue);
        vertRayCount = Mathf.Clamp(vertRayCount, 2, int.MaxValue);

        //Calc RaySpacing
        horRaySpacing = bounds.size.y / (horRayCount - 1);
        vertRaySpacing = bounds.size.x / (vertRayCount - 1);
    }
    #endregion

    #region Structs
    struct RayCastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 botLeft, botRight;
    }
    public struct CollisionInfo
    {
        public bool below, above;
        public bool right, left;

        public void ResetColl()
        {
            above = below = false;
            right = left = false;
        }

    }
    #endregion
}
