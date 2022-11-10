using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChat : Collidable
{
    public string message;
    // public Color color;
    private float chatLastShown = -4.0f;
    private float chatCooldown = 4.0f;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player" && coll.name != "Weapon")
            return;
        if (Time.time - chatLastShown > chatCooldown)
        {
            chatLastShown = Time.time;
            GameManager.instance.ShowText(message, 25, Vector3.up * 20, chatCooldown, transform.position + new Vector3(0, 0.16f, 0), Color.white);
        }
    }
}
