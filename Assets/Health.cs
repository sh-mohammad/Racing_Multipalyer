using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    const int MaxHealth = 100;
    public int CurrentHealth = MaxHealth;
    public bool Is_Enemy = true;
    public bool DestroyOnDead = true;
    public void TakeDamage(GameObject PlayerFrom, int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            if (DestroyOnDead)
            {
                Destroy(gameObject);
            }
            else
            {
                //samething

            }
        }
        //TODO network
    }
}
