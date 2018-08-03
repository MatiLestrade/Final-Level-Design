using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    private GameObject _player;
    public Transform newPos;
    public GameObject terrain1;
    public GameObject terrain2;
    // Use this for initialization
    void Start () {
        terrain2.SetActive(false);
          _player = GameObject.Find("FPSController");
        if (_player != null) Debug.Log("sarasa");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider a)
    {
        if (a.transform.tag == "Player")
        {
            terrain2.SetActive(true);
            terrain1.SetActive(false);
            _player.transform.position = newPos.position;
        }
    }
}
