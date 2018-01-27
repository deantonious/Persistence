using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float Speed = 3;
	public int PlayerState = 0;
	private int XStepCounter = 0;
	private int YStepCounter = 0;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (PlayerState == 0)
				PlayerState = 1;
			else if (PlayerState == 1)
				PlayerState = 2;
			else if (PlayerState == 2)
				PlayerState = 0;
		}
	}

	void FixedUpdate() {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));

		if (PlayerState == 0) {
			float axisX = Input.GetAxis("Horizontal");
			Vector3 force = new Vector3(axisX, 0, 0);
			
			Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;

			if (newTransform.x > -camScreen.x && newTransform.x < camScreen.x && newTransform.y > -camScreen.y && newTransform.y < camScreen.y)
				transform.position = newTransform;

		} else if (PlayerState == 1) {
			float axisX = Input.GetAxis("Horizontal");
			float axisY = Input.GetAxis("Vertical");

			if (YStepCounter > 30) {
				Vector3 force = new Vector3(axisX, 0, 0);
				Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;
				if (newTransform.x > -camScreen.x && newTransform.x < camScreen.x && newTransform.y > -camScreen.y && newTransform.y < camScreen.y) {
					transform.position = newTransform;
				}
				YStepCounter = 0;
			} else {
				Vector3 force = new Vector3(0, axisY, 0);
				Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;
				if (newTransform.x > -camScreen.x && newTransform.x < camScreen.x && newTransform.y > -camScreen.y && newTransform.y < camScreen.y) {
					transform.position = newTransform;

				}
				YStepCounter++;
			}

			
		} else {

		}
		
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(gameObject);
	}
	

}
