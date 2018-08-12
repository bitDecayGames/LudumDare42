using System;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    public float DefaultBlackHoleMovementSpeed = 2f;
    public GameObject PathMarkers;
    
    private Queue<PathNode> _nodeQueue;
    private PathNode _currentNode;

    private bool _done;
    
    private void Start()
    {
        _nodeQueue = new Queue<PathNode>();

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
            BlackHoleSpeed speed = child.gameObject.GetComponent<BlackHoleSpeed>();

            PathNode node = new PathNode();
            node.position = child.transform.position;
            node.speed = speed ? speed.Speed : DefaultBlackHoleMovementSpeed;

            _nodeQueue.Enqueue(node);
            Destroy(child.gameObject);
        }

        if (_nodeQueue.Count == 0)
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
        
        transform.position = Vector3.MoveTowards(transform.position, _currentNode.position, _currentNode.speed * Time.deltaTime);

        if (VectorMath.AreVectorsEqual(transform.position, _currentNode.position))
        {
            if (_nodeQueue.Count > 0)
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
        _currentNode = _nodeQueue.Dequeue();
    }
}

struct PathNode
{
    public Vector3 position;
    public float speed;
}