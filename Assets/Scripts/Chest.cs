using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    //The Empty sprite to swap the full chest spite with a empty one.
    public Sprite emptyChest;
    //public amount of pesos which is given to the player
    public int pesosAmount = 10;

    //Overrided method to check if the chest is not already collected and then doing stuff accordingly.
    protected override void OnCollect()
    {
        if (!collected)
        {
            //chest has been collected so make it true
            collected = true;
            //Get the renderer component on the object and change it's sprite to empty chest, u can also make it null if u want to make the object dissapear.
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 25, Vector3.up * 60, 1.0f, transform.position, Color.yellow);
        }
    }
}
