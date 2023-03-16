using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Interactables;
using TMPro;
namespace MightBMaybe.Cleone.UI {
    public class ChangeTextOnButton : MonoBehaviour
    {
        Button button;
        TextMeshProUGUI text;
        public string UnactiveText;
        public string ActiveText;
        // Start is called before the first frame update
        void Start()
        {
            button = GetComponentInParent<Button>();
            text = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = button.et.trigger.isTriggered ? ActiveText : UnactiveText;
        }
    } 
}
