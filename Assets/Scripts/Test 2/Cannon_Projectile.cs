using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Projectile : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "TargetTank"){
            // Debug.Log("Target Hit");
            Destroy(gameObject);
            
        }
    }
    
}
