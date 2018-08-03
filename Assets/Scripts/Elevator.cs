using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    private float _timer;
    private int _speed;
    private bool _active;
	// Use this for initialization
	void Start () {
        _speed = 10;

    }

    // Update is called once per frame
    void Update() {
        if (_active) _timer += Time.deltaTime;
        if (_timer >= 1 && _active) transform.position += Vector3.up * Time.deltaTime * _speed;
        if (_timer >= 11.2f) { _active = false;}
       
    }
    public void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag == "Player")
        {
            if(_timer <= 0)
            _active = true;
        }
    }
}
