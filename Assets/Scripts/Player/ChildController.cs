using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public Sprite nonKeySprite;
    public Sprite keySprite;
    public RuntimeAnimatorController nonKeyAnimatorController;
    public RuntimeAnimatorController keyAnimatorController;

    public Vector2 bottomOffset;

    void Update()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (PlayerController.Instance.haveKey)
        {
            spriteRenderer.sprite = keySprite;
            animator.runtimeAnimatorController = keyAnimatorController;
        }
        else
        {
            spriteRenderer.sprite = nonKeySprite;
            animator.runtimeAnimatorController = nonKeyAnimatorController;
        }
    }
    // Adjust the collider when dying
    public void AdjustCollider()
    {
        var collider = GetComponent<CapsuleCollider2D>();
        if (collider != null)
        {
            collider.size = new Vector2(1f, 1f);
        }
    }

    // Change to the old when anim over
    public void ChangeToOld()
    {
        PlayerController.Instance.ChangeForm(true);
    }

    // Set the hurting to flase
    public void OverHurt()
    {
        PlayerController.Instance.OverHurt();

    }

}
