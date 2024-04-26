using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{

    public Vector2 bottomOffset;

    public void AdjustCollider()
    {
        var collider = GetComponent<CapsuleCollider2D>();
        if (collider != null)
        {
            collider.size = new Vector2(1f, 1f);
        }
    }

    public void ChangeToOld()
    {
        PlayerController.Instance.ChangeForm(true);
    }

    public void OverHurnt()
    {
        PlayerController.Instance.OverHurt();

    }

}
