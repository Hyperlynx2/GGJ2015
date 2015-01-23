using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public enum role
	{
		spy, handler,
	}
	public role myRole;
	public Camera[] secCams;
	public GameObject theSpy;
	private int camIndex = 0;
	// Use this for initialization
	void Start () 
	{
		//if I'm not the spy, disable the spy.
		if(myRole == role.handler)
		{
			theSpy.SetActive(false);
		}
		//start with the cameras off.
		foreach(Camera cam in secCams)
		{
			cam.enabled = false;
		}
		//turn the first one on.
		secCams [0].enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//the mechanics methods are running out of the update function based on the role determined, so as not to bloat the update function.
		//im a handler, these are my mechanics
		if(myRole == role.handler)
		{
			HandlerMechanics();
		}

		//im the spy, these are my mechanics
		if(myRole == role.spy)
		{
			SpyMechanics();
		}
	}

	//HACKER SECTION
	void HandlerMechanics()
	{
		//press tab to cycle through the cameras
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			secCams[camIndex].enabled = false;
			//cycle through as many cameras as we have.
			if(camIndex < secCams.Length-1)
			{
				camIndex ++;
			}
			else
			{
				camIndex = 0;
			}
			secCams[camIndex].enabled = true;
		}
	}

	//SPY SECTION
	void SpyMechanics()
	{

	}
}
