using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    public GameObject PlayerFrom;
    private void OnTriggerEnter(Collider other)
    {
        {
            GameObject hit = other.gameObject;
            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(PlayerFrom, 10);
                Destroy(gameObject);
            }


        }
    }
    

}
