using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTriangular : MonoBehaviour {

	public float speed;
	private Vector3 force = new Vector3 ();
	void Start ()
	{
		// default right
		force.Set (1, 1, 1);
	}

	// Update is called once per frame
	void FixedUpdate () {
		float axisX = Input.GetAxis("Horizontal");
		float axisY = Input.GetAxis("Vertical");

		// We need to check if he wants to go up or down
		// up
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))  {
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				// left
				force.Set (-1, 1, 1);
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				// right
				force.Set (1, 1, 1);
			}
		}

		// down
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				force.Set (-1, -1, 1);
			}

			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				force.Set (1, -1, 1);
			}
			
		}

		// movement (A,D,Left,Right)
		this.transform.position = this.transform.position - force * (-speed) * Time.deltaTime;
	}
}
