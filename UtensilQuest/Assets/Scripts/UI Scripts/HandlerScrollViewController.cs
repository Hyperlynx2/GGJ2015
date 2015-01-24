using UnityEngine;
using System.Collections;

public class HandlerScrollViewController : MonoBehaviour {

    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlayerConnected(NetworkPlayer newPlayer)
    {
        Debug.Log("Player: " + newPlayer.ipAddress);
    }
}
