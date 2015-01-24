using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class AgentStartGame : MonoBehaviour
{
    public Text PlayersConnectedText;

    private int uiNumPlayersConnected = 0;

    void Start()
    {
        Network.incomingPassword = "Dadnewt";
        Network.InitializeServer(32, 16048, false);
    }

    // Use this for initialization
    public void OnStartButtonClicked()
    {
        networkView.RPC("RPC_OnServerStartGame", RPCMode.Others, null);
        Application.LoadLevel(PlayerPrefs.GetString("LEVEL_TO_LOAD"));
    }

    void OnPlayerConnected(NetworkPlayer newPlayer)
    {
        uiNumPlayersConnected++;

        if (uiNumPlayersConnected > 4)
        {
            Network.CloseConnection(newPlayer, true);
            return;
        }

        PlayersConnectedText.text = "Number of Connected Handlers: " + uiNumPlayersConnected;
        networkView.RPC("RPC_SetLevelToLoad", RPCMode.Others, PlayerPrefs.GetString("LEVEL_TO_LOAD"));
        //Send the new level to the player
    }

    void OnPlayerDisconnected(NetworkPlayer newPlayer)
    {
        uiNumPlayersConnected--;
        PlayersConnectedText.text = "Number of Connected Handlers: " + uiNumPlayersConnected;
    }

    //Fix RPC errors
    [RPC]
    void RPC_OnServerStartGame() { }
    [RPC]
    void RPC_SetLevelToLoad(string levelName) { }
}
