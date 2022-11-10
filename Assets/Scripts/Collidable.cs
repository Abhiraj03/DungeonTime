using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    //Filter to know what exactly we should collide with!
    public ContactFilter2D filter;
    //private as the object will have the component and we will initialize it in Awake call.
    private BoxCollider2D boxCollider;
    //An array to contain data of how many number of hits or things the object is colliding per frame.
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        //This makes it so that whatever the script is attached to will need a BoxCollider2D else it will throw an error.
        // You can also add this on top which will automatically add the component to the object:
        // [RequireComponent(typeof(BoxCollider2D))]
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        //The Overlap method checks if there are any collider above or behith the object's collider and puts them into the array.
        boxCollider.OverlapCollider(filter, hits);
        //Loop through the hits and see if it hit something or its null and then empty out the array each time.
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    //Doing this way makes it so u can call this function to check if the collision is done by a particular object, such as Player.
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not implemented in " + this.name);
    }
}