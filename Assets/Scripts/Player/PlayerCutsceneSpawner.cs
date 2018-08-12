﻿using UnityEngine;

namespace Player
{
    public class PlayerCutsceneSpawner : MonoBehaviour
    {

        public Rigidbody2D SpawnLocationPrefab;
        public Vector2 ShootVector;
        public float ShootDelay;
        public float OpenDelay;
        private Animator hatch;

        private float _hatchDelay;
        private float _shootTimer;
        private bool shot = false;
        private Rigidbody2D bullet;

        private enum SpawnState
        {
            START,
            SPAWN,
            CLOSE,
            DONE
        }

        private SpawnState currentState;

        // Use this for initialization
        void Start()
        {
            currentState = SpawnState.START;
            _hatchDelay = OpenDelay;
            _shootTimer = ShootDelay;
            hatch = GetComponentInChildren<Animator>();
            hatch.Play("Idle");
        }

        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case SpawnState.START:
                    _hatchDelay -= Time.deltaTime;
                    if (_hatchDelay <= 0)
                    {
                        _hatchDelay = OpenDelay;
                        Debug.Log("OPEN STATE HIT");
                        hatch.Play("Open");
                        currentState = SpawnState.SPAWN;
                    }
                    break;
                case SpawnState.SPAWN:
                    if (_shootTimer < 0)
                    {
                        Debug.Log("SHOOTING");
                        bullet = Instantiate(SpawnLocationPrefab);
                        bullet.transform.position = transform.position;
                        bullet.velocity = ShootVector;
                        currentState = SpawnState.CLOSE;
                    }
                    else _shootTimer -= Time.deltaTime;
                    break;
                case SpawnState.CLOSE:
                    _hatchDelay -= Time.deltaTime;
                    if (_hatchDelay <= 0)
                    {
                        Debug.Log("CLOSE STATE HIT");
                        hatch.Play("Close");
                        currentState = SpawnState.CLOSE;
                    }
                    break;
                case SpawnState.DONE:
                    break;
            }
        }
    }
}
