using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
	public GameObject[] obsPrefabs;
	public GameObject[] rareObsPrefabs;

	public float SpawnDelay;

	public int SpanwAtZ = 5;

	void Start () {
		InvokeRepeating("SpawnObstacle", SpawnDelay, SpawnDelay);
	}

	private void SpawnObstacle() {
		int marginFromCenter = 10, marginFromSides = 20;

		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
		Vector3 spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 1));
		spawn.z = SpanwAtZ;

		if (Random.Range(0, 100) < 80) {
			spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(marginFromSides, (Screen.width/2) - marginFromCenter), Screen.height + Random.Range(0, 150), 1));
			spawn.z = SpanwAtZ;
			GameObject obs0 = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;

			spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range((Screen.width / 2) + marginFromCenter, Screen.width - marginFromSides), Screen.height + Random.Range(0, 150), 1));
			spawn.z = SpanwAtZ;
			if (Random.Range(0, 100) < 90) {
				GameObject obs1 = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
			} else {
				GameObject rareObs = Instantiate(rareObsPrefabs[Random.Range(0, rareObsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
			}
		} else {
			GameObject rareObs = Instantiate(rareObsPrefabs[Random.Range(0, rareObsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
		}
		
	}
}
