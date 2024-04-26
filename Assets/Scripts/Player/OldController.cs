using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldController : MonoBehaviour
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

    // Make the old model seems toe according to adjust the old collider
    public void OldCollider1()
    {
        var collider = GetComponent<CapsuleCollider2D>();
        if (collider != null)
        {
            collider.size = new Vector2(1.407032f, 2.54f);
            collider.offset = new Vector2(-0.08464217f, -0.02f);
        }
    }

    public void OldCollider2()
    {
        var collider = GetComponent<CapsuleCollider2D>();
        if (collider != null)
        {
            collider.size = new Vector2(1.407032f, 2.505263f);
            collider.offset = new Vector2(-0.08464217f, 0f);
        }
    }

    // Change to the child when the anim over
    public void ChangeToChild()
    {
        PlayerController.Instance.ChangeForm(false);
    }

}
