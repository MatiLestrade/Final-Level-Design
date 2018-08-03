using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPos : MonoBehaviour {
    public Transform posReset;
    private GameObject _player;
	// Use this for initialization
	void Start () {
        _player = GameObject.Find("FPSController");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider aa)
    {
        if(aa.transform.tag == "Player")
        {
            _player.transform.position = posReset.position;
        }
    }
}
