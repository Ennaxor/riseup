using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Animator anim;
	Direction currentDir;
	Vector2 input;
	Vector3 startPos;
	Vector3 endPos;
	bool isWalking = false;
	float fracjurney;
	public float walkSpeed = 5f;

	public bool isAllowedToMove = true;

	void Start () {
		anim = GetComponent<Animator> ();
		isAllowedToMove = true;
	}

	void Update () {
		input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (input == Vector2.zero) {
			isWalking = false;
			anim.SetBool ("isWalking", isWalking);
		}

		if(isAllowedToMove)
		{
			
			if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
				input.y = 0;
			else
				input.x = 0;

			if (input != Vector2.zero) {

				if (input.x < 0) {
					currentDir = Direction.West;
				}
				if (input.x > 0) {
					currentDir = Direction.East;
				}
				if (input.y < 0) {
					currentDir = Direction.South;
				}
				if (input.y > 0) {
					currentDir = Direction.North;
				}
				anim.SetFloat ("x", input.x);
				anim.SetFloat ("y", input.y);
				StartCoroutine (Move (transform));
			} 

		}
			
	}

	public IEnumerator Move(Transform entity) {
		isAllowedToMove = false;
		isWalking = true;
		anim.SetBool ("isWalking", isWalking);
		startPos = entity.position;
		fracjurney = 0;
		endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

		while (fracjurney < 1f)
		{
			fracjurney += Time.deltaTime*walkSpeed;
			entity.position = Vector3.Lerp(startPos, endPos, fracjurney);
			yield return null;
		}
		isAllowedToMove = true;
		yield return 0;
	}
}

enum Direction
{
	North,
	East,
	South,
	West
}