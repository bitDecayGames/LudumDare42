using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StatsAggregator : MonoBehaviour
{
    private static StatsAggregator instance;
	
    public static StatsAggregator Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                instance = gameObject.AddComponent<StatsAggregator>();
                gameObject.name = "StatsAggregator";
            }

            return instance;
        }
    }

    private SceneStatsData _sceneStatsData;
	
    public HashSet<string> KeySet = new HashSet<string>();
	
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        gameObject.AddComponent<RestApiAccessor>();
        DontDestroyOnLoad(gameObject);
    }

    private SceneStatsData GetSceneStatsData()
    {
        if (_sceneStatsData != null)
        {
            return _sceneStatsData;
        }
        _sceneStatsData = SceneStatsData.GetLocalInstance();
        return _sceneStatsData;
    }
	
    private void Update()
    {
		
        foreach (char c in Input.inputString)
        {
            try
            {
                if (Input.GetKeyDown(c.ToString()))
                {
                    KeySet.Add(c.ToString());
                }
            } 
            // We don't care if this call fails
            catch
            {
						
            }
        }
    }
	
    private void OnApplicationQuit()
    {
        StatsObject statsObject = new StatsObject
        {
            Platform = Application.platform.ToString(),
            KeysPressed = KeySet.ToArray(),
            GameCompleted = false,
            Id = GetSceneStatsData().SessionId,
            Level = GetSceneStatsData().Level,
            LevelsCleared = new []{"1","2"},
            LocationOnQuit = GetSceneStatsData().Player.transform.position,
            TimePlayedSeconds = Time.time,
            DeathCount = 0,
            Score = 23453,
        };
		
        GetComponent<RestApiAccessor>().SendStats(statsObject);
    }
}