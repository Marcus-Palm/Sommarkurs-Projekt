using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private cameraFollow cam;


    [SerializeField] Transform nextDestination;


    [Space(30)]

    [SerializeField] Transform newMinPos;

    [SerializeField] Transform newMaxPos;

    void Start()
    {
        cam = Camera.main.GetComponent<cameraFollow>();
    }

    public void TpPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = nextDestination.position;

        ChangeCamConstraints();

    }

    public void ChangeCamConstraints()
    {
        cam.minPos = newMinPos.position;
        cam.maxPos = newMaxPos.position;
    }
}
