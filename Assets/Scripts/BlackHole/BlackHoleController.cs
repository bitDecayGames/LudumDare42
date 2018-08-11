using System;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    private static float BlackHoleMovementSpeed = 1f;
    
    private List<Vector3> _nodelist;
    private Vector3 _currentTarget;

    private bool _done;
    
    private void Start()
    {
        _nodelist = new List<Vector3>();
        
        foreach(Transform child in transform)
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