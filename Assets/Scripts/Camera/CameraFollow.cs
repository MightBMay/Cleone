using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Player;
public class CameraFollow : MonoBehaviour
{
    Transform player;
    
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.pMove.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
