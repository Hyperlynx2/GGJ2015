using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour 
{
	public GameObject[] inventory;
	public Texture[] invIcons;
	public int currentSlot = 0;
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
		Debug.Log (hit.name);
		//check what we hit is in fact an item
		if(hit.gameObject.GetComponent<Item>())
		{
			inventory[currentSlot] = hit.gameObject;
			invIcons[currentSlot] = hit.gameObject.GetComponent<Item>().icon;
			if(currentSlot < inventory.Length)
			{
				currentSlot ++;
			}
		}
	}
}
