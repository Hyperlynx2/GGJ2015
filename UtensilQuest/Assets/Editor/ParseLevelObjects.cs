using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseLevelObjects : MonoBehaviour
{
    static List<List<Transform>> enemyPathList = null;


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
        //Reset the state of the world for things that we will be regenerating
        GameManager manager = GameManager.FindObjectOfType<GameManager>();
        manager.secCams.Clear();

        //Destroy all enemies in the level
        Enemy[] enemyList = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy obj in enemyList)
        {
            GameObject.DestroyImmediate(obj.gameObject);
        }

        //Empty the old path list
        enemyPathList = new List<List<Transform>>();

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

            if(obj.name.Contains("PLAYER-START"))
            {
                HandlePlayerSpawnPoint(obj);
            }
            if(obj.name.Contains("PATHNODE"))
            {
                HandlePathNode(obj);
            }
        }

        CreateEnemiesForPaths();
    }

    private static void CreateEnemiesForPaths()
    {
        foreach(List<Transform> enemyPath in enemyPathList)
        {

            Object enemyPrefab = Resources.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");
            GameObject enemyObj = PrefabUtility.InstantiatePrefab(enemyPrefab) as GameObject;

            Enemy scr = enemyObj.GetComponent<Enemy>();
            scr.waypoints = enemyPath.ToArray();


            enemyObj.transform.position = enemyPath[0].position;
        }
    }

    private static void HandlePathNode(GameObject obj)
    {
        //Parse the path node for information
        string pathID = obj.name.Substring("PATHNODE-".Length, 3);
        string nodeValue = obj.name.Substring("PATHNODE-xxx-".Length, 3);

        int iPathID = int.Parse(pathID);
        int iNodeValue = int.Parse(nodeValue);


        while(iPathID > enemyPathList.Count)
        {
            enemyPathList.Add(null);
        }

        if(enemyPathList[iPathID - 1] == null)
        {
            enemyPathList[iPathID - 1] = new List<Transform>();
        }

        
        while(iNodeValue > enemyPathList[iPathID - 1].Count)
        {
            enemyPathList[iPathID - 1].Add(null);
        }

        enemyPathList[iPathID - 1][iNodeValue - 1] = obj.transform;
    }

    private static void HandlePlayerSpawnPoint(GameObject obj)
    {
        GameManager manager = GameManager.FindObjectOfType<GameManager>();
        manager.spawnPoint = obj.transform;

        EditorUtility.SetDirty(manager);
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

        Camera cam = obj.GetComponent<Camera>();
        if (cam == null)
        {
            cam = obj.AddComponent<Camera>();
        }

        AudioListener listener = obj.GetComponent<AudioListener>();
        if (listener == null)
        {
            listener = obj.AddComponent<AudioListener>();
        }

        GameManager manager = GameManager.FindObjectOfType<GameManager>();
        manager.secCams.Add(cam);

        EditorUtility.SetDirty(obj);
        Debug.Log("Added a camera!");
    }

    private static void AttachCollidableObject(GameObject obj, bool bKeepValues)
    {
        BoxCollider col = obj.GetComponent<BoxCollider>();

        if (col == null)
        {
            col = obj.AddComponent<BoxCollider>();
        }

        EditorUtility.SetDirty(obj);
        Debug.Log("Added a collidable");
    }

}
