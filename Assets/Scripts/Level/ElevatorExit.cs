using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorExit : MonoBehaviour
{
    bool isActivated = false;
    public Transform start;
    public Transform to;
    public float speed;
    Transform player;
    // Start is called before the first frame update

    private void Start()
    {
        start = transform.parent.Find("Start");
        to = transform.parent.Find("To");
        player = GameObject.Find("PlayerBody").transform;

    }
    private void Update()
    {
        MoveElevator();
    }
    /// <summary>
    /// Moves the elevator from its starting position downwards. Locks player's position to the elevators.
    /// </summary>
    public void MoveElevator()
    {
        if (isActivated && transform.position.y >= to.position.y-0.5f)
        {
            transform.position += Vector3.down * speed *Time.deltaTime;
            player.position = new Vector3(player.position.x, transform.position.y + 1.1f, player.position.z);

        }

    }
    /// <summary>
    /// Starts elevator when player enters the trigger.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
        }
    }
}
