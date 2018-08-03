using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : MonoBehaviour {
    public GameObject lightDoor;
    public GameObject _player;
    public AudioClip sound;
    private bool _move;
	private bool _soundBool;

	private float _timer;
    private float _speed;
	// Use this for initialization
	void Start ()
    {
        _player = GameObject.Find("FPSController");
        _speed = 2;
        lightDoor.SetActive(false);
		_soundBool = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (_move) { transform.position += Vector3.down * Time.deltaTime * _speed; _timer += Time.deltaTime; }
        if(_timer >= 1.5f) { _move = false; lightDoor.SetActive(true);  }
    }
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag == "Player")
        {
            if(_timer <= 0)
            _move = true;
        }
    }
	private void OnTriggerEnter(Collider a)
	{
		if (a.gameObject.tag == "Player"&& _soundBool==false)
		{
			AudioSource.PlayClipAtPoint(sound, _player.transform.position, 100);
			_soundBool = true;
			Elevator.button = true;
		}
	}
}
