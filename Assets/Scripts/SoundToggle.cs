using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private void Awake() {
        var obj = GameObject.FindGameObjectWithTag("Music");
        GetComponent<Image>().sprite =
                obj.GetComponent<AudioSource>().isActiveAndEnabled ? soundOnSprite : soundOffSprite;
    }

    public void ToggleSound() {
        if (GetComponent<Image>().sprite == soundOnSprite) {
            var objs = GameObject.FindGameObjectsWithTag("Music");
            foreach (var o in objs) {
                o.GetComponent<AudioSource>().enabled = false;
            }
            GetComponent<Image>().sprite = soundOffSprite;
        }
        else {
            var objs = GameObject.FindGameObjectsWithTag("Music");
            foreach (var o in objs) {
                o.GetComponent<AudioSource>().enabled = true;
            }
            GetComponent<Image>().sprite = soundOnSprite;
        }
    }
}
