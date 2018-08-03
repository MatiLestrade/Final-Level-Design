using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFire : MonoBehaviour {

	public Transform spawn;
	public Transform spawn2;
	public GameObject fire;
	private GameObject _player;
	public bool button;
	void Start ()
	{

		fire.SetActive(false);
		_player = GameObject.Find("FPSController");
		fire.transform.position = transform.position;
		
	}
	
	
	void Update ()
	{
		button = Elevator.button;
	}

	private void OnTriggerStay(Collider a)
	{
		if (a.gameObject.tag == "Player"&& button== false)
		{
			_player.transform.position = spawn.position;
		}
		if (a.gameObject.tag == "Player"&& button==true)
		{
			_player.transform.position = spawn2.position;
		}
	}
}
