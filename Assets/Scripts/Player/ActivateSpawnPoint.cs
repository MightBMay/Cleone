using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;

namespace MightBMaybe.Cleone.Player
{
    /// <summary>
    /// Keeps track of whether or not this SpawnPoint has been activated.
    /// </summary>
    public class ActivateSpawnPoint : MonoBehaviour
    {
        public bool isActive;

        /// <summary>
        /// Activates this spawn point when player enters trigger area.
        /// </summary>
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
