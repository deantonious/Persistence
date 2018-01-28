using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

    public void QuitScene() {
        Debug.Log("Close game");
        Application.Quit();
    }
}
