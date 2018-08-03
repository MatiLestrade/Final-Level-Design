using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public GameObject footSteps;
    public GameObject _player;
    // Use this for initialization
    void Awake()
    {
     _player = GameObject.Find("FPSController");
        footSteps.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(footSteps.transform.position,_player.transform.position) < 60f)
        {

        footSteps.SetActive(true);
        }
        else footSteps.SetActive(false);
    }
}
