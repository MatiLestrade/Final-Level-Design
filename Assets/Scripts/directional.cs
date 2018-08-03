using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directional : MonoBehaviour {
    public GameObject directionalLight;
    private float _timer;
    private bool _active;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_active)
        {
            directionalLight.transform.Rotate(new Vector3(345.942f, 0, 0));
            _active = false;
        }
                   }
    void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag == "Player")
        {
            _active = true;
        }
    }
}
