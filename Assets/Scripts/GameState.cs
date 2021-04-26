using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameState : MonoBehaviour {
    [SerializeField] private GameOverDisplay gameOverDisplay;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ScoreTracker scoreTracker;
    [SerializeField] private TerrainHandler terrainHandler;

    [SerializeField] private Plane plane;
    
    private CameraFollow camFollow;

    private Vector3 startPos;
    
    private void Awake() {
        camFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void Start() {
        startPos = plane.transform.position;
    }

    public void GameOver() {
        gameOverDisplay.UpdateScore(scoreTracker.Score);
        gameOverDisplay.SetActive(true);
        scoreTracker.gameObject.SetActive(false);
    }
    
    public void PlayAgain() {
        sceneLoader.TriggerTransition();
        Invoke(nameof(Restart), sceneLoader.transitionTime);
    }

    public void Restart() {
        gameOverDisplay.SetActive(false);
        terrainHandler.Restart();
        var newPlane = Instantiate(plane);
        newPlane.gameState = this;
        Transform trans = newPlane.transform;
        trans.position = startPos;
        camFollow.Follow(trans);
        scoreTracker.Restart(trans);
    }
    
    public void NewCave() {
        sceneLoader.StartGame();
    }
    
    public void Quit() {
        sceneLoader.GameOver();
    }
}
