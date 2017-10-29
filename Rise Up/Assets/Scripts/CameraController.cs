using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Player currentHero;
    Vector3 targetPos;
    void FindHero() {
        var heroes = FindObjectsOfType<Player>();
        foreach (var h in heroes)
        {
            currentHero = h;
        }
    }

    void Start() {
        FindHero();
    }

    Vector3 vel;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {

        transform.position = currentHero.transform.position + offset;
    }

    void Update() {


//        targetPos = currentHero.transform.position + Vector3.Scale((Vector3)currentHero.LookFacing, new Vector3(1.5f, 1f, 0f));
 //       targetPos.y += .5f;
  //      targetPos.z -= 1.5f;
   //     transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, .45f);
//        transform.position = targetPos;

    }


}
