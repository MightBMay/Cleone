using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MightBMaybe.Cleone.Interactables
{
    public class DoorInfo : MonoBehaviour
    {
        /// <summary>
        /// starting position of the door.
        /// </summary>
        public Transform start;
        /// <summary>
        /// position of door after being opened.
        /// </summary>
        public Transform to;

        public bool isOpen;
        /// <summary>
        /// speed of door closing/opening.
        /// </summary>
        public float speed;
        // Start is called before the first frame update

        private void Start()
        {
            start = transform.parent.Find("Start");
            to = transform.parent.Find("To");

        }
        /// <summary>
        /// Moves door to its open/closed position depending on its state.
        /// </summary>
        public void MoveDoor()
        {
            if (isOpen)
            {
                transform.position = start.position;
                isOpen = false;

            }
            else
            {
                transform.position = to.position;
                isOpen = true;
            }
        }
    }
}
