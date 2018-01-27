using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollBackground : MonoBehaviour {

    public GameObject fondo;
    GameObject uno;
    GameObject dos;

    public float speed;


	// Use this for initialization
	void Start () 
    {
        uno = GameObject.Instantiate(fondo, new Vector3(0, (float)10.584, 15), Quaternion.identity);

        dos = GameObject.Instantiate(fondo, new Vector3(0,0, 15), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
    {
        uno.transform.position = Vector3.Lerp(uno.transform.position, new Vector3(0, uno.transform.position.y - 1), speed);
        dos.transform.position = Vector3.Lerp(dos.transform.position, new Vector3(0, dos.transform.position.y - 1), speed);



        if (uno.transform.position.y < -10.584f)
            uno.transform.position = new Vector3(0, dos.transform.position.y + (float)10.584);

        if (dos.transform.position.y < -10.584f)
            dos.transform.position = new Vector3(0, uno.transform.position.y + (float)10.584);
    }
}
