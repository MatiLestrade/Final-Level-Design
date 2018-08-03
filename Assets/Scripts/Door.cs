using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public GameObject particle;
    private bool _active;
	// Use this for initialization
	void Start () {
        particle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (_active) particle.SetActive(true);
	}
}
