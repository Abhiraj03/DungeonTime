using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    // public ParticleSystem dust;
    private bool isAlive = true;

    // protected override void UpdateMotor(Vector3 input)
    // {
    //     base.UpdateMotor(input);
    //     if (isMoving)
    //         CreateDust();
    // }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void RecieveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.RecieveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoints++;
        hitpoints = maxHitpoints;
    }

    public void SetLevel(int level)
    {
        for (int i = 1; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {
        if (hitpoints == maxHitpoints)
            return;
        hitpoints += healingAmount;
        if (hitpoints > maxHitpoints)
        {
            hitpoints = maxHitpoints;
        }
        GameManager.instance.ShowText("+" + healingAmount.ToString(), 25, Vector3.up * 30, 1.0f, transform.position, Color.magenta);
        GameManager.instance.OnHitPointChange();
    }

    public void Respawn()
    {
        Heal(maxHitpoints);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    // public void CreateDust()
    // {
    //     dust.Play();
    // }
}
