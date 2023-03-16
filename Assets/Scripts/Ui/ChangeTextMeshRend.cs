using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MightBMaybe.Cleone.Interactables;

namespace MightBMaybe.Cleone.UI
{
    public class ChangeTextMeshRend : MonoBehaviour
    {
        MeshRenderer mesh;
        TextMeshProUGUI text;
        public string UnactiveText;
        public string ActiveText;
        // Start is called before the first frame update
        void Start()
        {
            mesh = GetComponentInParent<MeshRenderer>();
            text = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            
            text.text = mesh.enabled ? ActiveText : UnactiveText;
            text.text = text.text.Replace("\\n", System.Environment.NewLine);
        }
    }
}
