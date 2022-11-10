using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitpoints = 10;
    public int maxHitpoints = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //Push 
    protected Vector3 pushDirection;

    //All fighters can Receive Damage / Die
    protected virtual void RecieveDamage(Damage dmg)
    {
        //if the cooldown to hit is over then reset the lastImmune and deduct the hitpoints along with calculating the push Direction and showing a text. Also if hitpoints reaach below 0 or 0 make it die.
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Vector3.up * 30, 0.5f, transform.position, Color.red);

            if (hitpoints <= 0)
            {
                hitpoints = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}

