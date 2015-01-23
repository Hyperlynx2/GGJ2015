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
        if (err == NetworkConnectionError.NoError)
        {
            ConnectedNotification.enabled = true;
            ErrorNotifaction.enabled = false;

            ConnectButton.enabled = false;
        }
        else
        {
            ConnectedNotification.enabled = false;
            ErrorNotifaction.enabled = true;
        }
    }

    [RPC]
    void RPC_OnServerStartGame(string someInfo)
    {
        Application.LoadLevel("4 - MainGame");
    }
}
