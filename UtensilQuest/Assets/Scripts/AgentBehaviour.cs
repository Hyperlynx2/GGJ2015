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
		/*if(currentSlot >= inventory.Length)
		{
			currentSlot = 0;
		}*/
	}
}
