using UnityEngine;
using System.Collections;

public class SelectLevel : MonoBehaviour {

	// Use this for initialization
	public void OnTestLevelButtonClicked()
    {
        PlayerPrefs.SetString("LEVEL_TO_LOAD", "4 - Test Level");
        Application.LoadLevel("3 - AgentLobby");
    }

    public void OnLevelOneButtonClicked()
    {
        PlayerPrefs.SetString("LEVEL_TO_LOAD", "4 - Level 1");
        Application.LoadLevel("3 - AgentLobby");
    }

    public void OnLevelTwoButtonClicked()
    {
        PlayerPrefs.SetString("LEVEL_TO_LOAD", "4 - Level 2");
        Application.LoadLevel("3 - AgentLobby");
    }
}
