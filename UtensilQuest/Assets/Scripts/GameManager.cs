using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//this script determines what role you are and then sets up your mechanics accordingly
public class GameManager : MonoBehaviour
{
	public bool OculusGame = false;
    public enum role
    {
        spy, handler,
    }
    [HideInInspector] public role myRole;
    public List<SecurityCamera> secCams = new List<SecurityCamera>();
	[HideInInspector] public GameObject theSpy;
    private int camIndex = 0;
    public GameObject OculusPlayer;
    public GameObject NonOcPlayer;
    public Transform spawnPoint;
	private bool Typing;
	private int firstDigit;
	private int secondDigit;
	private float typingTimer;
	public int typingWindow;
	public Text FirstDigText;
	public Text SecondDigText;
	public Text CurCamText;
	public GameObject food;
	public GameObject[] cutlery;
    // Use this for initialization
    void Start()
    {
        if (Network.isServer) //I'm the spy, spawn me.
        {
            myRole = role.spy;
			if(OculusGame)
			{
				theSpy = Network.Instantiate (OculusPlayer, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
			}
			else
			{
				theSpy = Network.Instantiate (NonOcPlayer, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
			}
			foreach (SecurityCamera cam in secCams)
			{
				cam.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
				cam.GetComponent<AudioListener>().enabled = false;
			}
			Destroy (GameObject.Find("CameraGUI"));
			//spawn the pickups and food
			Instantiate (food, GameObject.Find("FOOD-LOCATION").transform.position, Quaternion.identity);
			GameObject[] pickupSpots = GameObject.FindGameObjectsWithTag("PickupPos");
			for(int i = 0; i < cutlery.Length; i ++)
			{
				Instantiate(cutlery[i], pickupSpots[i].transform.position, Quaternion.identity);
			}
		}
        else  //I'm the handler, set up the cameras
        {
            myRole = role.handler;
			//start with the cameras off.
			foreach (SecurityCamera cam in secCams)
			{
				cam.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
				cam.GetComponent<AudioListener>().enabled = false;
                //cam.GetComponent<SecurityCamera>().miniMapImage.enabled = false;
			}
			//turn the first one on.
            secCams[0].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = true;
            secCams[0].GetComponent<AudioListener>().enabled = true;
            //secCams[0].GetComponent<SecurityCamera>().miniMapImage.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //the mechanics methods are running out of the update function based on the role determined, so as not to bloat the update function.
        //im a handler, these are my mechanics
        if (myRole == role.handler)
        {
            HandlerMechanics();
        }

        //im the spy, these are my mechanics
        if (myRole == role.spy)
        {
            SpyMechanics();
        }
    }

    //HACKER SECTION
    void HandlerMechanics()
    {
		//double check the player is turned off.
		TurnOffPlayer ();

		//handle the timer for typing your chosen camera
		if(Typing)
		{
			FirstDigText.text = firstDigit.ToString();
			SecondDigText.text = secondDigit.ToString();
			typingTimer += Time.smoothDeltaTime;
			if(typingTimer >= typingWindow || Input.GetKeyDown(KeyCode.Return))
			{
				//determine the number chosen and select that camera
				typingTimer = 0;
				SetCam(ProcessNumber(firstDigit,secondDigit));
			}
		}

        //press tab to cycle through the cameras
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            secCams[0].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
            secCams[camIndex].GetComponent<AudioListener>().enabled = false;
            secCams[camIndex].GetComponent<SecurityCamera>().miniMapImage.enabled = false;
            //cycle through as many cameras as we have.
            if (camIndex < secCams.Count - 1)
            {
                camIndex++;
            }
            else
            {
                camIndex = 0;
            }
            secCams[0].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = true;
            secCams[camIndex].GetComponent<AudioListener>().enabled = true;
            secCams[camIndex].GetComponent<SecurityCamera>().miniMapImage.enabled = true;
        }
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
            secCams[0].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
            secCams[camIndex].GetComponent<AudioListener>().enabled = false;
            secCams[camIndex].GetComponent<SecurityCamera>().miniMapImage.enabled = false;
			//cycle through as many cameras as we have.
			if (camIndex > 0)
			{
				camIndex --;
			}
			else
			{
				camIndex = secCams.Count - 1;
			}

            secCams[camIndex].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = true;
            secCams[camIndex].GetComponent<AudioListener>().enabled = true;
            secCams[camIndex].GetComponent<SecurityCamera>().miniMapImage.enabled = true;
		}

        if (Input.GetKeyDown(KeyCode.Alpha1))	//I pressed 1
        {
			//if I haven't started typing, now I am so it's the first digit.
            if(!Typing)
			{
				Typing = true;
				firstDigit = 1;
			}
			else //obviously I want the second digit
			{
				secondDigit = 1;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
        }
		if (Input.GetKeyDown(KeyCode.Alpha2))	//I pressed 2
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 2;
			}
			else //obviously I want the second digit
			{
				secondDigit = 2;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))	//I pressed 3
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				firstDigit = 3;
			}
			else //obviously I want the second digit
			{
				secondDigit = 3;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))	//I pressed 4
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 4;
			}
			else //obviously I want the second digit
			{
				secondDigit = 4;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))	//I pressed 5
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 5;
			}
			else //obviously I want the second digit
			{
				secondDigit = 5;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))	//I pressed 6
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 6;
			}
			else //obviously I want the second digit
			{
				secondDigit = 6;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))	//I pressed 7
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 7;
			}
			else //obviously I want the second digit
			{
				secondDigit = 7;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha8))	//I pressed 8
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 8;
			}
			else //obviously I want the second digit
			{
				secondDigit = 8;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha9))	//I pressed 9
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 9;
			}
			else //obviously I want the second digit
			{
				secondDigit = 9;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha0))	//I pressed 0
		{
			//if I haven't started typing, now I am so it's the first digit.
			if(!Typing)
			{
				Typing = true;
				firstDigit = 0;
			}
			else //obviously I want the second digit
			{
				secondDigit = 0;
				//pass the digits to the function and then set the camera
				typingTimer = 0;
			}
		}
    }

	//this turns the two numbers we typed into a double digit number
	int ProcessNumber(int a, int b)
	{
		int camNum = (a * 10) + b;
		//Debug.Log (camNum);
		firstDigit = 0;
		secondDigit = 0;
		FirstDigText.text = "_";
		SecondDigText.text = "_";
		Typing = false;
		return camNum;
	}

    //sets the camera to the one we want.
    void SetCam(int CamNumber)
    {
		if(secCams.Count - 1 >= CamNumber && secCams[CamNumber] != null)
		{
            secCams[0].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
	        secCams[camIndex].GetComponent<AudioListener>().enabled = false;
            secCams[camIndex].GetComponent<SecurityCamera>().miniMapImage.enabled = false;
	        camIndex = CamNumber;
            secCams[0].transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = true;
	        secCams[camIndex].GetComponent<AudioListener>().enabled = true;
            secCams[camIndex].GetComponent<SecurityCamera>().miniMapImage.enabled = true;
			if(camIndex < 10)
			{
				CurCamText.text = "0"+camIndex.ToString();
			}
			else
			{
				CurCamText.text = camIndex.ToString();
			}
		}
		else
		{
			return;
		}
    }

	//fallback function
	void TurnOffPlayer()
	{
		if (theSpy == null) //set theSpy if it's null, just to avoid errors, especially with the next part.
		{
			if(GameObject.Find("Player(Clone)"))
			{
				theSpy = GameObject.Find("Player(Clone)");
			}
			else
			{
				return;
			}

			//turn off the Oculus prefab components if we're playing an oculus game
			if(OculusGame)
			{
				theSpy.SetActive(false);
				theSpy.GetComponent<OVRGamepadController>().enabled = false;
				theSpy.GetComponent<OVRPlayerController>().enabled = false;
				theSpy.GetComponent<OVRMainMenu>().enabled = false;
				theSpy.GetComponentInChildren<OVRCameraRig>().enabled = false;
				theSpy.GetComponentInChildren<OVRManager>().enabled = false;
				theSpy.GetComponentInChildren<OVRScreenFade>().enabled = false;
			}
			else //or the fps controller components if we're not
			{
				theSpy.GetComponent<MouseLook>().enabled = false;
				theSpy.GetComponent<CharacterMotor>().enabled = false;
				theSpy.GetComponent<FPSInputController>().enabled = false;
			}
			//turn off the player's cameras
			foreach (Camera cam in theSpy.GetComponentsInChildren<Camera>())
			{
                cam.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
			}
		}
	}

    //SPY SECTION
    void SpyMechanics()
    {

    }




    //Networking section
    void OnPlayerConnected(NetworkPlayer connectingPlayer)
    {
        //We don't accept your kind here (during the game, anyway)
        Network.CloseConnection(connectingPlayer, true);
        Debug.Log("I just kicked: " + connectingPlayer.ipAddress);
    }

    //Client disconnection - just go back to intro scene
    void OnDisconnectedFromServer(NetworkDisconnection disconnect)
    {
        Application.LoadLevel("1-Splash");
    }
}
