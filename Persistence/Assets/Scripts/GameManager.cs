using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameManager : MonoBehaviour {

	public GameObject rightBorder;
	public GameObject leftBorder;
	public Light mainLight;
	public int Score;

	void Start () {

		InvokeRepeating("AddScore", 1, 1);
		ChangeLightColor(new Color(Random.Range(0, 255), 103, 178));
	}
	
	void Update () {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		leftBorder.transform.position = new Vector3(-camScreen.x, leftBorder.transform.position.y, 5);
		rightBorder.transform.position = new Vector3(camScreen.x, leftBorder.transform.position.y, 5);

	}

	public void ChangeLightColor(Color color) {
		mainLight.color = color;
	}

	private void AddScore() {
		Score++;
	}

	public void EndGame() {
		Information.Score = Score;
		SceneManager.LoadScene("GameOver");
	}
}
