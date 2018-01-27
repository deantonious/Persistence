using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ObstacleBehaviour : MonoBehaviour {
	

	void Update() {
		float FallSpeed = Information.ScrollSpeed;

		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));

		if (transform.position.y < -camScreen.y - 1) {
			Destroy(gameObject);
		}

		Vector3 force = new Vector3(0, -1, 0);
		transform.position = transform.position + force * FallSpeed * Time.deltaTime;
	}
}
