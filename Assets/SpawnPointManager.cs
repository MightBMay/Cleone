using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MightBMaybe.Cleone.Player
{
    public class SpawnPointManager : MonoBehaviour
    {
        public Transform[] SpawnPoints;
        Transform player;
        // Start is called before the first frame update
        void Start()
        {
            SpawnPoints = GetComponentsInChildren<Transform>().Except(new[] { transform }).ToArray(); ;
            player = GameObject.Find("PlayerBody").transform;
        }

        public Transform GetClosestSpawnPoint()
        {
            float cDist = Mathf.Infinity;
            Transform cTrans = SpawnPoints[0] ;
            foreach (Transform t in SpawnPoints)
            {
                float dist = Vector3.Distance(player.position, t.position);
                if (dist  < cDist)
                {
                    cTrans = t;
                    cDist = dist;
                }
            }
            return cTrans;
        }
    }
}
