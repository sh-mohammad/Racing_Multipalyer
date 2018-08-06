using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMissile : MonoBehaviour {
    public GameObject MissilePrefabe;
    public Transform MissileSpawn;
	// Use this for initialization
    public void Fire()
    {
        GameObject missile = Instantiate(MissilePrefabe, MissileSpawn.position, MissileSpawn.rotation);
        Debug.Log("instatiate missile");
        Missile m = missile.GetComponent<Missile>();
        m.PlayerFrom = this.gameObject;
        missile.GetComponent<Rigidbody>().velocity = missile.transform.forward * 6;
        Destroy(missile, 2.0f);
    }
}
