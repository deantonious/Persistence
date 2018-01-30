using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
	public GameObject[] obsPrefabs;
	public GameObject[] rareObsPrefabs;
	public int SpawnYOffset = 5;

	public float SpawnDelay;

	public int SpanwAtZ = 5;

	void Start () {
		InvokeRepeating("SpawnObstacle", SpawnDelay, SpawnDelay);
	}

	private void SpawnObstacle() {
		int marginFromCenter = 10, marginFromSides = 20;
		float r = Random.Range(0.95f, 1.2f);
		float rp = Random.Range(0.15f, 0.20f);

		GameObject obs;
		Vector3 camScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height + SpawnYOffset, 1));
		Vector3 spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 1));
		spawn.z = SpanwAtZ;

		if (Random.Range(0, 100) < 80) {
			spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(marginFromSides, (Screen.width/2) - marginFromCenter), Screen.height + SpawnYOffset + Random.Range(0, 150), 1));
			spawn.z = SpanwAtZ;
			obs = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
			
			obs.transform.localScale = new Vector3(r, r);

			spawn = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range((Screen.width / 2) + marginFromCenter, Screen.width - marginFromSides), Screen.height + SpawnYOffset  + Random.Range(0, 150), 1));
			spawn.z = SpanwAtZ;
			if (Random.Range(0, 100) < 90) {
				obs = Instantiate(obsPrefabs[Random.Range(0, obsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
				obs.transform.localScale = new Vector3(r, r);
			} else {
				obs = Instantiate(rareObsPrefabs[Random.Range(0, rareObsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
				obs.transform.localScale = new Vector3(rp, rp, rp);
			}
		} else {
			obs = Instantiate(rareObsPrefabs[Random.Range(0, rareObsPrefabs.Length)], spawn, Quaternion.identity) as GameObject;
			obs.transform.localScale = new Vector3(rp, rp, rp);
		}
		
	}
}
