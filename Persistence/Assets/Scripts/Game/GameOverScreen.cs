using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	public Text ScoreLabel;
	
	void Start () {
		ScoreLabel.text = "Best Score\n\n"+Information.BestScore+"\n\nScore\n\n" + Information.Score;
	}

	void Update() {
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return)) {
			Information.Score = 0;
			SceneManager.LoadScene("Intro");
		}
	}
}
