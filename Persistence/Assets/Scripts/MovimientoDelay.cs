using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDelay : MonoBehaviour {

	public float speed, delay, movement;
	private float axisX, axisY, time;
	private Vector3 force = new Vector3();
	void Start ()
	{}

	// Update is called once per frame
	void FixedUpdate () {
		time++;
		if (time > delay) {
			axisX = Input.GetAxis ("Horizontal");
			axisY = Input.GetAxis ("Vertical");
			force.Set (axisX, axisY, 0);

			// We need to check if he wants to go up or down
			// up
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))  {
				time = 0;
				force.Set (0, movement, 0);
			}
			// left
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				time = 0;
				force.Set (-movement, 0, 0);
			}
			// right
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				time = 0;
				force.Set (movement, 0, 0);
			}
			// down
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				time = 0;
				force.Set (0, -movement, 0);
			}
		}

		// movement (A,D,Left,Right)
		this.transform.position = this.transform.position - force * (-speed) * Time.deltaTime;
	}
}
