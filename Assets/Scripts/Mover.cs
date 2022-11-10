using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    //Get BoxCollider2D to get outside information and moveDelta to get the position each frame of the player
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    private Vector3 originalSize;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;
    // public bool isMoving = false;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        //Gives an error is the component is not added to the game object.
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reset the moveDelta.
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // if (moveDelta != null)
        //     isMoving = true;
        // else
        //     isMoving = false;

        //change the direction when player moves in different direcion
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-originalSize.x, originalSize.y, originalSize.z);

        //add pushforce to the moveDelta
        moveDelta += pushDirection;

        //decrease the push force
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Make sure we can move in this direction by casting a box there first, if the box returns null, then we are free to move.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make the player Move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make the player Move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
