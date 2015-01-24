using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
	public Texture icon;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.name == "Player(Clone)")
		{
			int currentInv = hit.GetComponentInChildren<AgentBehaviour>().currentSlot;
			hit.GetComponentInChildren<AgentBehaviour>().inventory[currentInv] = this.gameObject;
			hit.GetComponentInChildren<AgentBehaviour>().invIcons[currentInv] = icon;
			hit.GetComponentInChildren<AgentBehaviour>().currentSlot += 1;
			this.renderer.enabled = false;
			this.collider.enabled = false;
		}
	}
}
