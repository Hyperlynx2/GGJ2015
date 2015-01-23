using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandlerConnect : MonoBehaviour {
    public Text IPBox;

    public Text ConnectedNotification;
    public Text ErrorNotifaction;

    public Button ConnectButton;

	// Use this for initialization
	public void OnConnectButtonClicked()
    {
        NetworkConnectionError err = Network.Connect(IPBox.text, 16048, "Dadnewt");
        if (err != NetworkConnectionError.NoError)
        {
            ConnectedNotification.text = "";
            ErrorNotifaction.text = "Something went wrong with connection, errorcode: " + err.ToString();
        }
    }

    [RPC]
    void RPC_OnServerStartGame(string someInfo)
    {
        Application.LoadLevel("4 - MainGame");
    }

    void OnConnectedToServer()
    {

        ConnectedNotification.text = "Please wait for the game to start...";
        ErrorNotifaction.text = "";

        ConnectButton.interactable = false;
    }

    void OnDisconnectedFromServer(NetworkDisconnection disconnect)
    {

        ConnectedNotification.text = "You were disconnected (" + disconnect.ToString() + ") - Please reconnect";
        ErrorNotifaction.text = "";

        ConnectButton.interactable = true;
    }

    void OnFailedToConnect(NetworkConnectionError err)
    {
        ConnectedNotification.text = "";
        ErrorNotifaction.text = "Something went wrong with connection, errorcode: " + err.ToString();
    }
}
