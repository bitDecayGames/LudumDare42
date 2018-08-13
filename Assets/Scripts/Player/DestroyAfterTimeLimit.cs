using UnityEngine;

namespace Player
{
    public class DestroyAfterTimeLimit : MonoBehaviour
    {

        public float TimeLimit;

        public string MetricKey;

        private float _timeLimit;

        void OnEnable()
        {
            _timeLimit = TimeLimit;
        }

        void Update()
        {
            if (_timeLimit < 0)
            {
                if (!string.IsNullOrEmpty(MetricKey))
                {
                    var tracker = Camera.main.GetComponent<MetricTracker>();
                    if (tracker) tracker.AddToTracking(MetricKey);
                }
                Destroy(gameObject);
            }
            else _timeLimit -= Time.deltaTime;
        }

        public float timeLeft()
        {
            return _timeLimit;
        }
    }
}
