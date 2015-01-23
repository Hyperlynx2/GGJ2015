using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetPlayerIP : MonoBehaviour {
    public Text ipTextObject;
	// Use this for initialization
	void Start () {
        ipTextObject.text = Network.player.ipAddress;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
