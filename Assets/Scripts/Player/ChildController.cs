using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{

    public Vector2 bottomOffset;

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
