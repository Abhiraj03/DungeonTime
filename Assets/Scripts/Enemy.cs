using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience 
    public int xpValue;

    //Logic:
    public float triggerLength = 1; //to set the length in meter which triggers the enemy chase
    public float chaseLength = 5;//to set the length in meter which tells how far away to go to stop the chase from initial location.
    private bool chasing;//check to see if its chasing
    private bool collidingWithPlayer;//check to see if it is colliding with player, if yes then dont move stay there
    private Transform playerTransform;//to get the player's position
    private Vector3 startingPosition;//to store the initial location of the enemy.

    //HitBox
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    private ContactFilter2D filter;

    protected override void Start()
    {
        base.Start();
        //We can use the player's transform from any place but GameManager is good
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        //We need to get the boxCollider of the child of the current object
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Check if player is in the range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        //Loop through the hits and see if it hit player or its null and then empty out the array each time.
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Vector3.up * 60, 1.0f, transform.position, Color.green);
    }
}
