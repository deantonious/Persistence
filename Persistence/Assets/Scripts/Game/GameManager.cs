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
	public int TimeToAddScore = 2;

	void Start () {
		InvokeRepeating("AddScore", TimeToAddScore, TimeToAddScore);
		Information.ScrollSpeed = 2;
		InvokeRepeating("UpdateSpeed", 20, 20);
	}
	
	void Update () {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		leftBorder.transform.position = new Vector3(-camScreen.x, leftBorder.transform.position.y, LimitsAtZ);
		rightBorder.transform.position = new Vector3(camScreen.x, leftBorder.transform.position.y, LimitsAtZ);
	}

	private void AddScore() {
		Information.Score++;
	}

	private void UpdateSpeed() {
		if(Information.ScrollSpeed < 7)
			Information.ScrollSpeed += 0.1f;
	}
}
