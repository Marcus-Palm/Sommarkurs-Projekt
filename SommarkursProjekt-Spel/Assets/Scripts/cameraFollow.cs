using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public GameObject player;
    public float smoothing;
    public Vector2 minPos;
    public Vector2 maxPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if(transform.position != player.transform.position)
        {

            Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPos,smoothing);
        }

    }
}
