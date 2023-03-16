using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollecter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Collect()
    {
        transform.GetComponentInChildren<MeshRenderer>().enabled = false;
    }
    public void ResetCollect()
    {
        transform.GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
