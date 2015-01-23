using UnityEngine;
using System.Collections;

//this script determines what role you are and then sets up your mechanics accordingly
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
			cam.GetComponent<AudioListener>().enabled = false;
		}
		//turn the first one on.
		secCams [0].enabled = true;
		secCams [0].GetComponent<AudioListener>().enabled = true;
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
			secCams[camIndex].GetComponent<AudioListener>().enabled = false;
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
			secCams[camIndex].GetComponent<AudioListener>().enabled = true;
		}
		if(Input.GetKeyDown(KeyCode.Alpha1) && secCams.Length-1 >= 0 && secCams[0] != null)
		{
			SetCam(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && secCams.Length-1 >= 1 && secCams[1] != null)
		{
			SetCam(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && secCams.Length-1 >= 2 && secCams[2] != null)
		{
			SetCam(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) && secCams.Length-1 >= 3 && secCams[3] != null)
		{
			SetCam(3);
		}
		if(Input.GetKeyDown(KeyCode.Alpha5) && secCams.Length-1 >= 4 && secCams[4] != null)
		{
			SetCam(4);
		}
		if(Input.GetKeyDown(KeyCode.Alpha6) && secCams.Length-1 >= 5 && secCams[5] != null)
		{
			SetCam(5);
		}
		if(Input.GetKeyDown(KeyCode.Alpha7) && secCams.Length-1 >= 6 && secCams[6] != null)
		{
			SetCam(6);
		}
		if(Input.GetKeyDown(KeyCode.Alpha8) && secCams.Length-1 >= 7 && secCams[7] != null)
		{
			SetCam(7);
		}
		if(Input.GetKeyDown(KeyCode.Alpha9) && secCams.Length-1 >= 8 && secCams[8] != null)
		{
			SetCam(8);
		}
		if(Input.GetKeyDown(KeyCode.Alpha0) && secCams.Length-1 >= 9 && secCams[9] != null)
		{
			SetCam(9);
		}
	}

	//sets the camera to the one we want.
	void SetCam(int CamNumber)
	{
		secCams[camIndex].enabled = false;
		secCams[camIndex].GetComponent<AudioListener>().enabled = false;
		camIndex = CamNumber;
		secCams[camIndex].enabled = true;
		secCams[camIndex].GetComponent<AudioListener>().enabled = true;
	}

	//SPY SECTION
	void SpyMechanics()
	{

	}
}
