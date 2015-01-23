using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour 
{
	public GameObject[] inventory;
	public Texture[] invIcons;
	private int currentSlot = 0;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collider hit)
	{
		//check what we hit is in fact an item
		if(hit.GetComponent<Item>())
		{
			inventory[currentSlot] = hit.gameObject;

		}
	}
}
