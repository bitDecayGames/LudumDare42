using System;
using UnityEngine;

public class RestApiAccessor : MonoBehaviour
{
    public void SendStats(StatsObject statsObject) {
        var req = new RESTEasyRequest();
        req.Header("Content-Type", "application/json");
        req.Body(JsonUtility.ToJson(statsObject)).Url("http://127.0.0.1:9090").OnFailure(
            (responseMessage, responseCode) =>
            {
                if (responseCode == 0)
                {
                    Debug.Log("Unable to reach server");
                    return;
                }
                
                Debug.Log(String.Format("Error fulfilling REST request: {0}", responseMessage));
            });
        StartCoroutine(req.Put());
    }
}