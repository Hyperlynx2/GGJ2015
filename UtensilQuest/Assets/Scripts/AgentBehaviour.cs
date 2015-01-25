using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AgentBehaviour : MonoBehaviour 
{
	public GameObject[] inventory;
	public Sprite[] invIcons;
	public Image[] invGUI;
	public Text messageText;
	public Text timerText;
	public int currentSlot = 0;
	private float levelTimer;
	public int levelLength;
	private int timerDis;
	public string reason;
	// Use this for initialization
	void Start () 
	{
		if(!PlayerPrefs.HasKey("Reason"))
		{
			PlayerPrefs.SetString("Reason", "");
		}
		if(Network.isServer)
		{
			//get the amount of items in the 
			for(int i = 1; i<=invGUI.Length; i++)
			{
				invGUI[i-1] = GameObject.Find("PGUI"+i.ToString()).GetComponent<Image>();
			}
			messageText = GameObject.Find ("MessageText").GetComponent<Text>();
			timerText = GameObject.Find ("timer").GetComponent<Text>();
			levelTimer = levelLength;
		}
	}

	void UpdateImages()
	{
		for(int i = 0; i<invGUI.Length; i++)
		{
			invGUI[i].GetComponent<Image>().sprite = invIcons[i];
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(Network.isServer)
		{
			if(levelTimer <= 0)
			{
				messageText.text = "My food will be cold by now!";
				GetComponent<CharacterController>().enabled = false;
				reason = "You ran out of time!";
				Invoke("GameLose", 3.0f);
			}
			else
			{
				levelTimer -= Time.smoothDeltaTime;
				timerDis = Mathf.RoundToInt(levelTimer);
				string mins = (timerDis/60).ToString();
				string secs = (timerDis%60).ToString();
				if((timerDis%60) < 10)
				{
					secs = "0"+secs;
				}
				timerText.text = mins + ":" + secs;
			}
		}
	}

	void GameWin()
	{
		PlayerPrefs.SetString("Reason", reason);
		Application.LoadLevel ("5 - EndGame");
	}

	void GameLose()
	{
		PlayerPrefs.SetString("Reason", reason);
		Application.LoadLevel ("5 - EndGame");
	}
}
