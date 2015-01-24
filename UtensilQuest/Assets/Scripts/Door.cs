using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public bool isLocked;
	private float reLockTimer;
	public int ReLockTime;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isLocked)
		{
			reLockTimer += Time.smoothDeltaTime;
			if(reLockTimer >= ReLockTime)
			{
				reLockTimer = 0;
				Locking (true);
			}
		}
	}

	void Locking(bool lockStatus)
	{
		if(lockStatus == true)
		{
			isLocked = true;
			transform.Translate(Vector3.down * 20);
		}
		else
		{
			isLocked = false;
			transform.Translate(Vector3.up * 20);
		}
	}
}
