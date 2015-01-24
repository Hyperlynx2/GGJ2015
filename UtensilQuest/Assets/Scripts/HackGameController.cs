using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HackGameController : MonoBehaviour {

    public GameObject GameButtonNode;

    public float TotalTime = 20.0f;
    public int RequiredPoints = 10;

    public float BoardRandomizeTime = 1.0f;

    public Text TimeRemainingText;
    public Text PointsRemainingText;

    public Text HackStatus;
    public bool HackSuccessful;

    public float remainingTime;
    public float remainRandomizeTime;
    public int remainingPoints;

    private bool bHackRunning;

    private Button[] buttons;

	// Use this for initialization
	void Start () {
        remainingTime = TotalTime;
        remainingPoints = RequiredPoints;

        remainRandomizeTime = BoardRandomizeTime;

        bHackRunning = true;

        buttons = GameButtonNode.GetComponentsInChildren<Button>();


        RandomizeBoard();
	}

    private void RandomizeBoard()
    {
        foreach(Button b in buttons)
        {
            //Set them all black
            b.interactable = false;
        }

        int iRand = Random.Range(0, buttons.Length - 1);

        buttons[iRand].interactable = true;


        remainRandomizeTime = BoardRandomizeTime;
    }
	
	// Update is called once per frame
	void Update () {
	    if(bHackRunning)
        {         

            remainingTime -= Time.smoothDeltaTime;
            remainRandomizeTime -= Time.smoothDeltaTime;
            UpdateInfo();

            if(remainingPoints <= 0)
            {
                SuccessfullHack();
            }
            else if(remainingTime <= 0.0f)
            {
                FailedHack();
            }

            if(remainRandomizeTime <= 0.0f)
            {
                RandomizeBoard();
            }
        }
	}

    public void OnButtonClick()
    {
        remainingPoints--;
        RandomizeBoard();
    }

    private void FailedHack()
    {
        bHackRunning = false;
        HackStatus.text = "Hack Failed!";
        HackStatus.color = Color.red;
    }

    private void SuccessfullHack()
    {
        bHackRunning = false;
        HackStatus.text = "Hack Succeeded!";
        HackStatus.color = Color.black;
    }

    private void UpdateInfo()
    {
        //throw new NotImplementedException();
        TimeRemainingText.text = "Time Remaining:" + remainingTime.ToString("0.0");
        PointsRemainingText.text = "Points Remaining: " + remainingPoints;
    }

}
