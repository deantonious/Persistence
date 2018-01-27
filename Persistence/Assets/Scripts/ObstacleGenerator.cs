using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
	public GameObject[] obsPrefabs;
	public GameObject[] rareObsPrefabs;

	public int SpawnRate;

	public int MinHorizontal;
	public int MaxHorizontal;
	
	void Start () {
		InvokeRepeating("SpawnObstacle", SpawnRate, SpawnRate);
	}

	private void SpawnObstacle() {
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		Vector3 spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 1));
		spawn.z = 0;

		if (Random.Range(0, 100) < 85) {
			spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width/2), Screen.height + Random.Range(200, 500), 1));
			GameObject octo1 = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
			spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.width/2, Screen.width), Screen.height + Random.Range(200, 500), 1));
			GameObject octo2 = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
		} else {
			GameObject octo = Instantiate(rareObsPrefabs[Random.Range(0, rareObsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
		}
		
	}
}
