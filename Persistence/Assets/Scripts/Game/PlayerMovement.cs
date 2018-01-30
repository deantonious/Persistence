using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public enum PlayerMode { TRIANGLE, SQUARE, DEFAULT };

	public GameObject ScoreUp;

	public float Speed = 3;
	public float RegresionSpeed = 0.27f;

	public float SquareSpeed = 12f;
	public int Delay = 25;
	public float Movimiento = 0.18f;

	public PlayerMode Mode = PlayerMode.DEFAULT;

	public AudioClip[] Songs;
	public AudioClip Transition;
	public AudioClip DeathSound;

	public Light MainLight;

	private float prevX = 0f, prevY = 0f, axisX = 0, axisY = 0;
	private bool isMoving = false;
	private int time = 0;

	void Start() {
		ChangePlayerMode(Mode);
	}

	void Update() {
		//Change movement mode
		if (Input.GetKeyDown(KeyCode.I)) {
			ChangePlayerMode(PlayerMode.DEFAULT);
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			ChangePlayerMode(PlayerMode.TRIANGLE);
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			ChangePlayerMode(PlayerMode.SQUARE);
		}
	}

	void FixedUpdate() {
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		Vector3 force = new Vector3();

		//Collides with top
		if (transform.position.y >= camScreen.y + 0.4) {
			Debug.Log("Collided with top");
			transform.position = new Vector3(0, 0, 0);
			EndGame();
		}

		//Collides with bottom
		if (transform.position.y <= -camScreen.y - 0.4) {
			Debug.Log("Collided with bottom");
			transform.position = new Vector3(0, 0, 0);
			EndGame();
		}

		//Collides with left
		if (transform.position.x <= -camScreen.x + 0.4) {
			Debug.Log("Collided with left");
			transform.position = new Vector3(0, 0, 0);
			EndGame();
		}

		//Collides with right
		if (transform.position.x >= camScreen.x - 0.4) {
			Debug.Log("Collided with right");
			transform.position = new Vector3(0, 0, 0);
			EndGame();
		}

		//Light Mode
		if (Mode == PlayerMode.DEFAULT) {
			if (!Input.anyKey) {
				if (gameObject.transform.position.x > 0.1 || gameObject.transform.position.x < -0.1) {
					if (gameObject.transform.position.x > 0) {
						force = new Vector3(-RegresionSpeed, 0, 0);
					} else {
						force = new Vector3(RegresionSpeed, 0, 0);
					}
				}
			} else {
				float axisX = Input.GetAxis("Horizontal");
				force = new Vector3(axisX, 0, 0);
			}

			Vector3 newTransform = transform.position - force * (-Speed) * Time.deltaTime;
			transform.position = newTransform;

		} else if (Mode == PlayerMode.SQUARE) {

			if (time >= Delay && isMoving) {
				isMoving = false;
				prevX = 0f;
				prevY = 0f;
				axisX = 0f;
				axisY = 0f;
			}

			if (Input.GetKeyDown(KeyCode.A) && !isMoving) {
				isMoving = true;
				axisX = -Movimiento;
				axisY = 0f;
				time = 0;
				prevX = axisX;
				prevY = axisY;
			}

			if (Input.GetKeyDown(KeyCode.D) && !isMoving) {
				isMoving = true;
				axisX = Movimiento;
				axisY = 0f;
				time = 0;
				prevX = axisX;
				prevY = axisY;
			}

			if (Input.GetKeyDown(KeyCode.S) && !isMoving) {
				isMoving = true;
				axisX = 0f;
				axisY = -Movimiento;
				prevX = axisX;
				prevY = axisY;
				time = 0;
			}

			if (Input.GetKeyDown(KeyCode.W) && !isMoving) {
				isMoving = true;
				axisX = 0f;
				axisY = Movimiento;
				time = 0;
				prevX = axisX;
				prevY = axisY;
			}

			axisX = prevX;
			axisY = prevY;

			force = new Vector3(axisX, axisY, 0);
			Vector3 newTransform = transform.position + force * SquareSpeed * Time.deltaTime;
			transform.position = newTransform;

			time++;


		} else if (Mode == PlayerMode.TRIANGLE) {
			//Triangular Mode	
			// We need to check if he wants to go up or down
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					force.Set(-1, 1, 0);
				} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					force.Set(1, 1, 0);
				}
			}
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

	public void ChangePlayerMode(PlayerMode mode) {
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		Animator PlayerAnim = GetComponent<Animator>();
		Mode = mode;
		if (mode == PlayerMode.DEFAULT) {
			gameObject.GetComponent<TrailRenderer>().enabled = false;

			gameObject.transform.localScale = new Vector3(0.3f, 0.3f);
			PlayerAnim.SetInteger("playerMode", 0);
			gameObject.transform.localScale = new Vector3(0.5f, 0.5f);

			gameObject.tag = "Light";
			MainLight.color = new Color(255, 255, 0);
			StartCoroutine(PlayUntilEnd(audio, Transition));
			audio.clip = Songs[0];
			audio.loop = true;
			audio.Play();
		} else if (mode == PlayerMode.TRIANGLE) {
			gameObject.GetComponent<TrailRenderer>().enabled = true;
			gameObject.GetComponent<TrailRenderer>().startColor = new Color(0, 95, 144);
			gameObject.GetComponent<TrailRenderer>().endColor = new Color(0, 95, 144);

			gameObject.transform.localScale = new Vector3(0.3f, 0.3f);
			PlayerAnim.SetInteger("playerMode", 2);
			gameObject.transform.localScale = new Vector3(0.6f, 0.6f);

			gameObject.tag = "Sound";
			MainLight.color = new Color(0, 95, 144);
			StartCoroutine(PlayUntilEnd(audio, Transition));
			audio.clip = Songs[2];
			audio.loop = true;
			audio.Play();
		} else if (mode == PlayerMode.SQUARE) {
			gameObject.GetComponent<TrailRenderer>().enabled = true;
			gameObject.GetComponent<TrailRenderer>().startColor = new Color(0, 171, 0);
			gameObject.GetComponent<TrailRenderer>().endColor = new Color(0, 171, 0);

			gameObject.transform.localScale = new Vector3(0.3f, 0.3f);
			PlayerAnim.SetInteger("playerMode", 1);
			gameObject.transform.localScale = new Vector3(0.6f, 0.6f);
			
			gameObject.tag = "Elec";
			MainLight.color = new Color(0, 171, 0);
			StartCoroutine(PlayUntilEnd(audio, Transition));
			audio.clip = Songs[1];
			audio.loop = true;
			audio.Play();
			Debug.Log("Mode changed to: " + mode);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (gameObject.tag != collision.tag) {
			Destroy(gameObject);
			Debug.Log("OnTriggerEnter " + collision);
			EndGame();
		} else {
			Destroy(collision.gameObject);
			Information.Score += 5;
			GameObject ob = Instantiate(ScoreUp, gameObject.transform.position, Quaternion.identity) as GameObject;
			Destroy(ob, 0.6f);
		}
	}

	public void EndGame() {
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		if (Information.BestScore < Information.Score)
			Information.BestScore = Information.Score;

		StartCoroutine(PlayUntilEnd(audio, DeathSound));
		SceneManager.LoadScene("GameOver");
	}

	IEnumerator PlayUntilEnd(AudioSource source, AudioClip audio) {
		source.clip = audio;
		source.Play();
		yield return new WaitForSeconds(audio.length);
		//yield return new WaitWhile(() => source.isPlaying);
	}
}
