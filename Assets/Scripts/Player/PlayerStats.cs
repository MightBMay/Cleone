using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;
namespace MightBMaybe.Cleone.Player
{


    /// <summary>
    /// Stores players stats.
    /// </summary>
    public  class PlayerStats : MonoBehaviour
    {
        public static PlayerStats pStats;
        public InputStats    IStats = new InputStats();
        public MovementStats MStats = new MovementStats();
        public CloneStats    CStats = new CloneStats();


        private void Awake()
        {
            pStats = this;
        }

        

    }

    /// <summary>
    /// Stores stats related to player input.
    /// </summary>
    [System.Serializable]
    public class InputStats
    {
        /// <summary>
        /// Speed of which the Camera rotates.
        /// </summary>
        public float lookSensitivity;
        /// <summary>
        /// How far (0 - 1) a joystick must be moved to register input.
        /// </summary>
        public float deadZone;
    }

    /// <summary>
    /// Stores stats related to player Movement.
    /// </summary>
    [System.Serializable]
    public class MovementStats
    {
        /// <summary>
        /// acceleration of player.
        /// </summary>
        public float moveSpeed;
        /// <summary>
        /// maximum speed of player.
        /// </summary>
        public float maxSpeed;
        /// <summary>
        /// deceleration of player when not holding any movement input.
        /// </summary>
        public float deceleration;
    }

    /// <summary>
    /// Stores stats related to Clones.
    /// </summary>
    [System.Serializable]
    public class CloneStats
    {
        /// <summary>
        /// current type of clone to spawn.
        /// </summary>
        public CloneTypes CurrentCloneType = null;
        /// <summary>
        /// how many clones the player can have at once.
        /// </summary>
        public float maxCloneCount;

    }
}
