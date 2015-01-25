using UnityEngine;
using System.Collections;

public class SplashScreenOnClick : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonUp(0))
        {
            Application.LoadLevel("1.5-PreGame");
        }
	}
}
