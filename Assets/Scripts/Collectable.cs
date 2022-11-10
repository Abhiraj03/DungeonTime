using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    //To keep in check if a chest or an object is collected or not.
    protected bool collected;

    //To check if the collision took place with the Player and nothing else
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
            OnCollect();
    }

    //Using this function as virtual will allow us to use it in Chest class to override it and grant pesos accordingly.
    protected virtual void OnCollect()
    {
        collected = true;
    }
}
