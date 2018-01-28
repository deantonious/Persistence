using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameManager : MonoBehaviour {

	public GameObject rightBorder;
	public GameObject leftBorder;

	public int LimitsAtZ = 5;
	public int PlayerZ = 5;
	public AudioClip DeathSound;

	void Start () {
		InvokeRepeating("AddScore", 1, 1);
	}
	
	void Update () {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		leftBorder.transform.position = new Vector3(-camScreen.x, leftBorder.transform.position.y, LimitsAtZ);
		rightBorder.transform.position = new Vector3(camScreen.x, leftBorder.transform.position.y, LimitsAtZ);

		if(Information.Score > 1) {
			Information.ScrollSpeed = Mathf.Log(Information.Score);
		}
		
	}

	private void AddScore() {
		Information.Score++;
	}

	public void EndGame() {
		if (Information.BestScore < Information.Score)
			Information.BestScore = Information.Score;
		SceneManager.LoadScene("GameOver");
		
	}
}
