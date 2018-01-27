using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforePlayerMovement : MonoBehaviour {
	public int speed = 3;
	private bool facingRight = true;
	private bool isDead = false;
	private Animator Animator;
	// Use this for initialization
	void Start() {
		Animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		float x = Input.GetAxis("Horizontal");


		Vector3 force = new Vector3(x, 0, 0);

		transform.position = transform.position + force * speed * Time.deltaTime;

		Animator.SetFloat("x", x);

		if (x >= 0) {
			facingRight = true;
		} else
			facingRight = false;

		if (facingRight)
			Animator.SetBool("facingRight", true);
		else
			Animator.SetBool("facingRight", false);

		isDead = Animator.GetBool("Dead");

		if (isDead) {
			speed = 0;
		}
	}


}
