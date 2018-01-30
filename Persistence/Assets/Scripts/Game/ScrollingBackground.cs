using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ScrollingBackground : MonoBehaviour {

	public GameObject fondo;
	GameObject uno, dos;

	public float speed;

	void Start() 
	{
		uno = GameObject.Instantiate(fondo, new Vector3(0, 10.584f, 15), Quaternion.identity);
		dos = GameObject.Instantiate(fondo, new Vector3(0, 0, 15), Quaternion.identity);
	}
	
	void Update() 
	{
		float speed = Information.ScrollSpeed * 0.025f;
		uno.transform.position = Vector3.Lerp(uno.transform.position, new Vector3(0, uno.transform.position.y - 1, 15), speed);
		dos.transform.position = Vector3.Lerp(dos.transform.position, new Vector3(0, dos.transform.position.y - 1, 15), speed);

		if (uno.transform.position.y < -10.584f)
			uno.transform.position = new Vector3(0, dos.transform.position.y + (float)10.584, 15);

		if (dos.transform.position.y < -10.584f)
			dos.transform.position = new Vector3(0, uno.transform.position.y + (float)10.584, 15);
	}
}
