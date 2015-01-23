using UnityEngine;
using System.Collections;

public class AgentStartGame : MonoBehaviour
{

    void Start()
    {
        Network.incomingPassword = "Dadnewt";
        Network.InitializeServer(32, 16048, false);
    }

    // Use this for initialization
    public void OnStartButtonClicked()
    {
        networkView.RPC("RPC_OnServerStartGame", RPCMode.Others, null);
        Application.LoadLevel("4 - MainGame");
    }

    //Fix RPC errors
    [RPC]
    void RPC_OnServerStartGame() { }
}
