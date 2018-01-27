using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public enum MovementMode { TRIANGLE, SQUARE, DEFAULT };

	public float Speed = 3;
	public MovementMode Mode = MovementMode.DEFAULT;
	public GameManager GameMng; 

	private int XStepCounter = 0;
	private int YStepCounter = 0;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (Mode == MovementMode.DEFAULT) {
				ChangeMovementMode(MovementMode.TRIANGLE);
			} else if(Mode == MovementMode.TRIANGLE) {
				ChangeMovementMode(MovementMode.SQUARE);
			} else if(Mode == MovementMode.SQUARE) {
				ChangeMovementMode(MovementMode.DEFAULT);
			}
		}
	}

	void FixedUpdate() {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		Vector3 force = new Vector3();

		//Collides with top
		if (transform.position.y >= camScreen.y) {
			Debug.Log("Collided with top");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Collides with bottom
		if (transform.position.y <= -camScreen.y) {
			Debug.Log("Collided with bottom");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Collides with left
		if (transform.position.x <= -camScreen.x) {
			Debug.Log("Collided with left");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Collides with right
		if (transform.position.x >= camScreen.x) {
			Debug.Log("Collided with right");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Light Mode
		if (Mode == MovementMode.DEFAULT) {
			float axisX = Input.GetAxis("Horizontal");
			force = new Vector3(axisX, 0, 0);
			
			Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;
			transform.position = newTransform;

		//Sound Mode
		} else if (Mode == MovementMode.SQUARE) {
			float axisX = Input.GetAxis("Horizontal");
			float axisY = Input.GetAxis("Vertical");

			if (YStepCounter > 30) {
				force = new Vector3(axisX, 0, 0);
				Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;
				transform.position = newTransform;

				YStepCounter = 0;
			} else {
				force = new Vector3(0, axisY, 0);
				Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;
				transform.position = newTransform;

				YStepCounter++;
			}

		//Triangular Mode	
		} else if(Mode == MovementMode.TRIANGLE) {
			// We need to check if he wants to go up or down
			// up
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					// left
					force.Set(-1, 1, 1);
				}
				if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					// right
					force.Set(1, 1, 1);
				}
			}

			// down
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					force.Set(-1, -1, 1);
				}

				if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					force.Set(1, -1, 1);
				}

			}

			// movement (A,D,Left,Right)
			this.transform.position = this.transform.position - force * (-Speed) * Time.deltaTime;
		}
		
		
	}

	public void ChangeMovementMode(MovementMode mode) {
		Mode = mode;
		Debug.Log("Mode changed to: " + mode);
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
