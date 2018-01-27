using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	public Text ScoreLabel;
	
	void Start () {
		ScoreLabel.text = "Score\n\n" + Information.Score;
	}

	void Update() {
		if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space)) {
			SceneManager.LoadScene("Intro");
		}
	}
}
