using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerMovement : MonoBehaviour {

	public enum MovementMode { TRIANGLE, SQUARE, DEFAULT };

	public float Speed = 3;
	public float SquareSpeed = 10;
	public MovementMode Mode = MovementMode.DEFAULT;
	public GameManager GameMng; 
	public AudioClip[] Songs;
	public Light mainLight;

	private float prevX = 0f, prevY = 0f, axisX = 0, axisY = 0;
	private bool isMoving = false;
	private int time = 0;
	public int delay = 25;
	public float speed = 12f, movimiento = 0.18f;

	void Start() {
		ChangeMovementMode(Mode);
	}

	void Update() {
		//Change movement mode
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ChangeMovementMode(MovementMode.DEFAULT);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			ChangeMovementMode(MovementMode.TRIANGLE);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			ChangeMovementMode(MovementMode.SQUARE);
		}
	}

	void FixedUpdate() {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		Vector3 force = new Vector3();

		//Collides with top
		if (transform.position.y >= camScreen.y+ 0.4) {
			Debug.Log("Collided with top");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Collides with bottom
		if (transform.position.y <= -camScreen.y- 0.4) {
			Debug.Log("Collided with bottom");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Collides with left
		if (transform.position.x <= -camScreen.x+ 0.4) {
			Debug.Log("Collided with left");
			transform.position = new Vector3(0, 0, 0);
			GameMng.EndGame();
		}

		//Collides with right
		if (transform.position.x >= camScreen.x-0.4) {
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

		
		} else if (Mode == MovementMode.SQUARE) {

			if (time >= delay && isMoving) {
				isMoving = false;
				prevX = 0f;
				prevY = 0f;
				axisX = 0f;
				axisY = 0f;
			}

			if (Input.GetKeyDown(KeyCode.A) && !isMoving) {
				isMoving = true;
				axisX = -movimiento;
				axisY = 0f;
				time = 0;
				prevX = axisX;
				prevY = axisY;
			}

			if (Input.GetKeyDown(KeyCode.D) && !isMoving) {
				isMoving = true;
				axisX = movimiento;
				axisY = 0f;
				time = 0;
				prevX = axisX;
				prevY = axisY;
			}

			if (Input.GetKeyDown(KeyCode.S) && !isMoving) {
				isMoving = true;
				axisX = 0f;
				axisY = -movimiento;
				prevX = axisX;
				prevY = axisY;
				time = 0;
			}

			if (Input.GetKeyDown(KeyCode.W) && !isMoving) {
				isMoving = true;
				axisX = 0f;
				axisY = movimiento;
				time = 0;
				prevX = axisX;
				prevY = axisY;
			}

			axisX = prevX;
			axisY = prevY;

			force = new Vector3(axisX, axisY, 0);
			Vector3 newTransform = transform.position + force * speed * Time.deltaTime;
			transform.position = newTransform;

			time++;


		} else if(Mode == MovementMode.TRIANGLE) {
			//Triangular Mode	
			// We need to check if he wants to go up or down
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					// left
				 	force.Set(-1, 1, 0);
				} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					// right
					force.Set(1, 1, 0);
				}
			}

			// down
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					force.Set(-1, -1, 0);
				} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					force.Set(1, -1, 0);
				}
			}

			// movement (A,D,Left,Right)
			this.transform.position = this.transform.position - force * (-Speed) * Time.deltaTime;
		}
		
		
	}

	public void ChangeMovementMode(MovementMode mode) {
		Mode = mode;
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		if (mode == MovementMode.DEFAULT) {
			mainLight.color = new Color(255, 255, 0);
			audio.clip = Songs[0];
			audio.Play();
		} else if (mode == MovementMode.SQUARE) {
			mainLight.color = new Color(0, 255, 0);
			audio.clip = Songs[1];
			audio.Play();
		} else if (mode == MovementMode.TRIANGLE) {
			mainLight.color = new Color(82, 60, 248);
			audio.clip = Songs[2];
			audio.Play();
		}
		Debug.Log("Mode changed to: " + mode);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Destroy(gameObject);
		Debug.Log("OnTriggerEnter " + collision);
		GameMng.EndGame();
	}

	

}
