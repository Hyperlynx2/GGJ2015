using UnityEngine;

using System;
using System.Collections;

public class NetworkTest : MonoBehaviour {
    bool bConnected = false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (!bConnected)
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Start Server"))
            {
                StartServer();
            }

            if (GUI.Button(new Rect(0, 30, 100, 30), "Start Client"))
            {
                StartClient();
            }
        }
    }

    private void StartClient()
    {
        Network.Connect("10.5.2.226", 16048, "Dadnewt");
        bConnected = true;

    }

    private void StartServer()
    {
        Network.incomingPassword = "Dadnewt";
        //var useNat = !Network.HavePublicAddress();

        Network.InitializeServer(32, 16048, false);
        bConnected = true;
    }
}
