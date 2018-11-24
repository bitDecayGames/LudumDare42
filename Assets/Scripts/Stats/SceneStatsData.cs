using System;
using UnityEngine;

public class SceneStatsData : MonoBehaviour
{
    private static String SceneStatsDataGameObjectName = "SceneStatsData";

    [HideInInspector] public string SessionId;
    public Transform Player;
    public String Level;

    private void Start()
    {
        SessionId = Guid.NewGuid().ToString();
        gameObject.name = SceneStatsDataGameObjectName;
    }

    private void Update()
    {
        if (Player == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                Player = player.transform;
            }
            Debug.Log(String.Format("Result of Player tag lookup: {0}", Player != null));
        }
    }

    public static SceneStatsData GetLocalInstance()
    {
        GameObject SceneStatsDataGameObject = GameObject.Find(SceneStatsDataGameObjectName);
        if (SceneStatsDataGameObject == null)
        {
            throw new Exception("Unable to find SceneStatsData game object. Is it in your scene?");
        }

        SceneStatsData sceneStatsData = SceneStatsDataGameObject.GetComponent<SceneStatsData>();        if (SceneStatsDataGameObject == null)
        {
            throw new Exception("Unable to find SceneStatsData on SceneStatsDataGameObject. Did you use the prefab?");
        }

        return sceneStatsData;
    }
}