using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Transform[] waypoints;
	private int wayIndex;
	public GameObject theSpy;
	public int speed;
	private float waitTimer;
	public int timeToWait;

	public enum states
	{
		patrolling, chasing, waiting,
	}
	public states myState;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//I'm currently on patrol
		if(myState == states.patrolling)
		{
			transform.LookAt(waypoints[wayIndex]);
			transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
			//once I get to my waypoint, wait a while.
			if(Vector3.Distance(transform.position, waypoints[wayIndex].position) < 1)
			{
				if(wayIndex < waypoints.Length-1)
				{
					wayIndex ++;
				}
				else
				{
					wayIndex = 0;
				}
				timeToWait = Random.Range(1,10);
				myState = states.waiting;
			}
		}
		//I'm currently waiting
		if(myState == states.waiting)
		{
			//wait for how long 
			waitTimer += Time.smoothDeltaTime;
			if(waitTimer >= timeToWait)
			{
				waitTimer = 0;
				myState = states.patrolling;
			}
		}
		//I'm chasing the player!
		if(myState == states.chasing)
		{

		}
	}
}
