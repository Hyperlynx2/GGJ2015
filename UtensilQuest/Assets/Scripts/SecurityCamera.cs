using UnityEngine;
using System.Collections;

//Controls the security camera, pans left and right with a delay and the ends.
public class SecurityCamera : MonoBehaviour 
{
	public float leftCap;
	public float rightCap;
	public float rotSpeed;
	private float waitTimer;
	public int waitFor;

	public enum rotDirection
	{
		left, right,
	}

	public rotDirection currentDirection;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//I'm panning left
		if(currentDirection == rotDirection.left)
		{
			//transform.rotation.Set(transform.rotation.x, transform.rotation.y + (rotSpeed * Time.smoothDeltaTime), 0, transform.rotation.w);
			if(transform.rotation.eulerAngles.y <= leftCap)
			{
				waitTimer += Time.smoothDeltaTime;
				if(waitTimer >= waitFor)
				{
					waitTimer = 0;
					currentDirection = rotDirection.right;
				}
			}
			else
			{
				transform.Rotate(Vector3.down * rotSpeed * Time.smoothDeltaTime);
			}
		}
		//I'm panning right
		if(currentDirection == rotDirection.right)
		{
			//transform.rotation.Set(transform.rotation.x, transform.rotation.y - (rotSpeed * Time.smoothDeltaTime), 0, transform.rotation.w);
			if(transform.rotation.eulerAngles.y >= rightCap)
			{
				waitTimer += Time.smoothDeltaTime;
				if(waitTimer >= waitFor)
				{
					waitTimer = 0;
					currentDirection = rotDirection.left;
				}
			}
			else
			{
				transform.Rotate(Vector3.up * rotSpeed * Time.smoothDeltaTime);
			}
		}
			
	}
}
