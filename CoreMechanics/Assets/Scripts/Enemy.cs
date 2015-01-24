using UnityEngine;
using System.Collections;

//controls the enemy ai, will patrol between points and wait a random length of time.
//It will chase the player if he's close enough, in front of him, and not on the other side of anything.
public class Enemy : MonoBehaviour 
{
	public Transform[] waypoints;
	private int wayIndex;
	public GameObject theSpy;
	public int patrolSpeed;
	public int chaseSpeed;
	private float waitTimer;
	public int timeToWait;
	private Vector3 distanceToPlayer;
	private float detectionDot;

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
	void FixedUpdate () 
	{
		if(Network.isClient)
		{
			return;
		}
		//if I can see the player, I'll chase him
		distanceToPlayer = theSpy.transform.position - transform.position;
		detectionDot = Vector3.Dot (distanceToPlayer.normalized, transform.TransformDirection (Vector3.forward)); 
		if(detectionDot > 0.5f && distanceToPlayer.magnitude < 10 && !Physics.Linecast(transform.position, theSpy.transform.position)) //and the linecast; add that later when I cbf with layermasking
		{
			myState = states.chasing;
		}

		//I'm currently on patrol
		if(myState == states.patrolling)
		{
			//look at the next waypoint, move towards it.
			transform.LookAt(waypoints[wayIndex]);
			transform.Translate(Vector3.forward * patrolSpeed * Time.smoothDeltaTime);
			//once I get to my waypoint, wait a while.
			if(Vector3.Distance(transform.position, waypoints[wayIndex].position) < 1)
			{
				//scroll through the array, make sure I loop back to the first element
				if(wayIndex < waypoints.Length-1)
				{
					wayIndex ++;
				}
				else
				{
					wayIndex = 0;
				}
				//determine how long I'll wait at the waypoint, then start waiting.
				timeToWait = Random.Range(1,4);
				myState = states.waiting;
			}
		}

		//I'm currently waiting
		if(myState == states.waiting)
		{
			//wait for how long I decided I'd wait (set in patrol state)
			waitTimer += Time.smoothDeltaTime;
			if(waitTimer >= timeToWait)
			{
				//reset my timer and go back to patrolling.
				waitTimer = 0;
				myState = states.patrolling;
			}
		}

		//I'm chasing the player!
		if(myState == states.chasing)
		{
			transform.LookAt(theSpy.transform.position);
			transform.Translate(Vector3.forward * chaseSpeed * Time.smoothDeltaTime);
			//condition to start patrolling again...
			//currentSpeed = patrolSpeed;
			//myState = states.patrolling;
		}
	}
}
