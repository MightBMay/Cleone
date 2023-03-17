using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MightBMaybe.Cleone.Player
{
    public class SpawnPointManager : MonoBehaviour
    {
        
        Transform player;
        // Start is called before the first frame update
        void Start()
        {
            
            player = GameObject.Find("PlayerBody").transform;
        }

        public Transform GetClosestSpawnPoint()
        {
            float cDist = Mathf.Infinity;
            ActivateSpawnPoint[] SpawnPoints = GetComponentsInChildren<ActivateSpawnPoint>();
            Transform cTrans = SpawnPoints[0].transform;
            
            foreach (ActivateSpawnPoint s in SpawnPoints)
            {
                if (s.isActive)
                {
                    float dist = Vector3.Distance(player.position, s.transform.position);
                    if (dist < cDist)
                    {
                        cTrans = s.transform;
                        cDist = dist;
                    }
                }
            }
            return cTrans;
        }
    }
}
