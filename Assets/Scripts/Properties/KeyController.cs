using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private Vector3 initialPos;
    private void Start()
    {
        initialPos=transform.position;
        YTEventManager.Instance.AddEventListener(EventStrings.AMNESIC_POINT, LossKey);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.haveKey = true;
            gameObject.SetActive(false);
        }
    }
    private void LossKey()
    {
        gameObject.SetActive(true);
        transform.position = initialPos;
        PlayerController.Instance.haveKey = false;
    }
    private void OnDestroy()
    {
        YTEventManager.Instance.RemoveEventListener(EventStrings.AMNESIC_POINT, LossKey);
    }
}
