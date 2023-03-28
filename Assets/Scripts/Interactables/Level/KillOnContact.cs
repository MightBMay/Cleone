using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Player;
using MightBMaybe.Cleone.Clones;

namespace MightBMaybe.Cleone.Interactables {
    /// <summary>
    /// i mean, cmon. pretty self explanatory.
    /// </summary>
    public class KillOnContact : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                PlayerMovement.pMove.RespawnPlayer();
                CloneManager.cloneManager.ResetClonesAndInteractables();
            }
            else if (collision.collider.CompareTag("Clone") || collision.collider.CompareTag("HeavyClone"))
            {
                CloneManager.cloneManager.clones.Remove(collision.gameObject);
                Destroy(collision.gameObject);
            }
        }
    } }
