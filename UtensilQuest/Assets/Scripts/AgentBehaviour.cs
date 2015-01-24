using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AgentBehaviour : MonoBehaviour 
{
	public GameObject[] inventory;
	public Sprite[] invIcons;
	public Image[] invGUI;
	public Text messageText;
	public int currentSlot = 0;
	// Use this for initialization
	void Start () 
	{
		for(int i = 1; i<=invGUI.Length; i++)
		{
			invGUI[i-1] = GameObject.Find("PGUI"+i.ToString()).GetComponent<Image>();
		}
		messageText = GameObject.Find ("MessageText").GetComponent<Text>();
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

	}
}
