using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] Transform mapOne;
    [SerializeField] Transform mapTwo;
    [SerializeField] Transform mapThree;

    [SerializeField] Transform mapOneBack;
    [SerializeField] Transform mapTwoBack;
    [SerializeField] Transform mapThreeBack;

    

    public void TpPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = mapOne.position;

    }
}
