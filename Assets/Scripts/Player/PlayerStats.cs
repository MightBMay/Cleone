using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;
namespace MightBMaybe.Cleone.Player
{



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


    [System.Serializable]
    public class InputStats
    {
        public float lookSensitivity;
        public float deadZone;
    }


    [System.Serializable]
    public class MovementStats
    {

        public float moveSpeed;
        public float maxSpeed;
        public float deceleration;
    }


    [System.Serializable]
    public class CloneStats
    {
        public CloneTypes CurrentCloneType = null;
        public float maxCloneCount;

    }
}
