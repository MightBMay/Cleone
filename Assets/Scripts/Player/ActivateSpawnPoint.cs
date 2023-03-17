using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;

namespace MightBMaybe.Cleone.Player
{
    public class ActivateSpawnPoint : MonoBehaviour
    {
        public bool isActive;


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                isActive = true;
                CloneManager.cloneManager.ResetClonesAndInteractables();
            }
        }
    }
}
