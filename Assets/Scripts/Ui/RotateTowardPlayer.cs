using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MightBMaybe.Cleone.UI
{

    public class RotateTowardPlayer : MonoBehaviour
    {
        Transform playerTransform;
        TextMeshProUGUI textMesh;
        // Start is called before the first frame update
        void Start()
        {
            playerTransform = GameObject.Find("PlayerBody").transform;
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 directionToPlayer = playerTransform.position - textMesh.transform.position;

            // Rotate the text to face the player
            textMesh.rectTransform.rotation = Quaternion.LookRotation(-directionToPlayer, Vector3.up);
        }
    }
}
