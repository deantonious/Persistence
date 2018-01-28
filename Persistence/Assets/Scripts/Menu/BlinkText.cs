using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlinkText : MonoBehaviour {


    Text StartText;

	// Use this for initialization
	void Start () {
        StartText = GetComponent<Text>();

        StartCoroutine(Blink());
		
	}

    public IEnumerator Blink()
    {
        while (true)
        {
            StartText.text = "";

           yield return new WaitForSeconds(.5f);

            StartText.text = "TOUCH TO PLAY";

            yield return new WaitForSeconds(.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
