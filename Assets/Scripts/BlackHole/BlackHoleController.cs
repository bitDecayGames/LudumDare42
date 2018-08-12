using System;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public float BlackHoleMovementSpeed = 1f;
    public GameObject PathMarkers;
    
    private List<Vector3> _nodelist;
    private Vector3 _currentTarget;

    private bool _done;
    
    private void Start()
    {
        _nodelist = new List<Vector3>();

        if (PathMarkers == null)
        {
            throw new Exception("No PathMarkers found");
        }
        if (!PathMarkers.name.Equals("PathMarkers"))
        {
            throw new Exception("Only the PathMarkers prefab can be set as the Path Markers object");
        }
        
        foreach(Transform child in PathMarkers.transform)
        {
            _nodelist.Add(child.transform.position);
            Destroy(child.gameObject);
        }

        if (_nodelist.Count == 0)
        {
            Debug.Log("No movement nodes detected");
            _done = true;
        }
        else
        {
            LoadNextNode();
        }

    }

    private void Update()
    {
        if (_done)
        {
            return;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, BlackHoleMovementSpeed * Time.deltaTime);

        if (VectorMath.AreVectorsEqual(transform.position, _currentTarget))
        {
            if (_nodelist.Count > 0)
            {
                LoadNextNode();
            }
            else
            {
                _done = true;
            }
        }
    }

    private void LoadNextNode()
    {
        Debug.Log("Loading in next node");
        _currentTarget = _nodelist[0];
        _nodelist.RemoveAt(0);
    }
}