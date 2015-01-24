using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public bool isLocked;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Locking (!isLocked);
		}
	}

	void Locking(bool lockStatus)
	{
		if(lockStatus == true)
		{
			isLocked = true;
			transform.Translate(Vector3.forward * transform.localScale.z);
		}
		else
		{
			isLocked = false;
			transform.Translate(Vector3.back * transform.localScale.z);
		}
	}
}
