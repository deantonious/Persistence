using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			StartGame();
		}
	}

	public void StartGame() {
        SceneManager.LoadScene("Intro");
    }

}
