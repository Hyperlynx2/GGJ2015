﻿using UnityEngine;
using System.Collections;

public class WinZone : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay(Collider hit)
	{
		if (hit.name == "Player(Clone)") //The player is inside 
		{
			if(hit.GetComponent<AgentBehaviour>().currentSlot == hit.GetComponent<AgentBehaviour>().invGUI.Length-1)
			{
				//WINRAR
				hit.GetComponent<AgentBehaviour>().messageText.text = "NOMS!";
			}
			else
			{
				//You still need utensils
				hit.GetComponent<AgentBehaviour>().messageText.text = "I can't eat this with my hands...";
			}
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.name == "Player(Clone)") //The player is inside 
		{
			hit.GetComponent<AgentBehaviour>().messageText.text = "";
		}
	}
}