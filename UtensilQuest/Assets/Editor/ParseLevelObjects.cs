﻿using UnityEditor;
using UnityEngine;
using System.Collections;

public class ParseLevelObjects : MonoBehaviour
{

    [MenuItem("Game/Parse Level Objects (Reset all values)")]
    static void DoFullLevelParse()
    {
        DoLevelParsing(false);
    }

    [MenuItem("Game/Parse Level Objects (Keep all Values)")]
    static void DoSemiLevelParse()
    {
        DoLevelParsing(true);
    }

    static void DoLevelParsing(bool bKeepValues)
    {
        GameObject[] goList = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in goList)
        {
            if (obj.name.Contains("SecurityCam"))
            {
                AttachSecurityCamera(obj, bKeepValues);
            }

            if (obj.name.Contains("COLLIDE"))
            {
                AttachCollidableObject(obj, bKeepValues);
            }
        }
    }

    private static void AttachSecurityCamera(GameObject obj, bool bKeepValues)
    {
        SecurityCamera scr = obj.GetComponent<SecurityCamera>();

        if (scr == null)
        {
            scr = obj.AddComponent<SecurityCamera>();
        }

        if (!bKeepValues)
        {
            scr.rightCap = obj.transform.eulerAngles.y;
            scr.leftCap = (360 + (obj.transform.eulerAngles.y - 90)) % 360; //Make sure this value is from 0-360
            scr.rotSpeed = 5;
            scr.waitFor = 1;
        }

        NetworkView net = obj.GetComponent<NetworkView>();

        if (net == null)
        {
            net = obj.AddComponent<NetworkView>();
        }


        Debug.Log("Added a camera!");
    }

    private static void AttachCollidableObject(GameObject obj, bool bKeepValues)
    {
        BoxCollider col = obj.GetComponent<BoxCollider>();

        if (col == null)
        {
            col = obj.AddComponent<BoxCollider>();
        }

        Debug.Log("Added a collidable");
    }

}