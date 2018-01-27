using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
	public int speed = 2;
	private bool isgrounded = false;
	private int xAxis = -1;

	public Animator personajePrincAnim;
	public Animator anim;
	public Image black;

	Rigidbody2D rb;

	bool isDead = false;
	void Start() {
		rb = this.GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update() {

		Vector3 force = new Vector3(xAxis, 0, 0);

		transform.position = transform.position + force * speed * Time.deltaTime;
		if (isgrounded) {
			rb.AddForce(new Vector2(0f, 150f));
			isgrounded = false;
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "ground") {
			isgrounded = true;
		}
		if (coll.gameObject.tag == "Player" && !isDead) {
			isDead = false;
			xAxis = 0;
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.Play();
			StartCoroutine(Fading());
		}
	}

	IEnumerator Fading() {
		anim.SetBool("Fade", true);
		personajePrincAnim.SetBool("Dead", true);
		yield return new WaitUntil(() => black.color.a > 0.8);
		SceneManager.LoadScene("Persistance");
	}
}
