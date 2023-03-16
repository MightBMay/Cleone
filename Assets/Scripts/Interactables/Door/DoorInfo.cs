using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour
{
    public Transform start;
    public Transform to;
    public bool isOpen;
    public float speed;
    // Start is called before the first frame update

    private void Start()
    {
        start = transform.parent.Find("Start");
        to = transform.parent.Find("To");

    }
    public void MoveDoor()
    {
        if (isOpen)
        {
            transform.position = start.position;
            isOpen = false;

        }
        else { 
            transform.position = to.position;
            isOpen = true;
        }
    }
}
