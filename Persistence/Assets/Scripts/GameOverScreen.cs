using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class GameOverScreen : MonoBehaviour {

	public Text ScoreLabel;
	
	void Start () {
		ScoreLabel.text = "Score: " + Information.Score;
	}
	
	void Update () {
		
	}
}
