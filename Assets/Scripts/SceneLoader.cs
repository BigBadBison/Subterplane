using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public float transitionTime = 1f;

    Animator transition;

    private void Awake() {
        transition = GetComponentInChildren<Animator>();
    }

    public void StartGame() {
        StartCoroutine(LoadScene(1));
    }

    public void GameOver() {
        StartCoroutine(LoadScene(0));
    }

    public void TriggerTransition() {
        StartCoroutine(Transition());
    }
    
    IEnumerator Transition() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("End");
    }
    
    IEnumerator LoadScene(int buildIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
