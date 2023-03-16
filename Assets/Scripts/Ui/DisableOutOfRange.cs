using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MightBMaybe.Cleone.UI
{

    public class DisableOutOfRange : MonoBehaviour
    {
        TextMeshProUGUI text;
        Transform player;
        public float Range;
        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            player = GameObject.Find("PlayerBody").transform;
        }

        // Update is called once per frame
        void Update()
        {
             text.enabled = ( Vector3.Distance(transform.position, player.position) >= Range )  ? false :true ; 
        }
    }
}
