using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //碰撞到人物及通关
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerController.Instance.haveKey)
            {
                //通关
                audioSource.Play();
                Debug.Log("Game Win");
                YTEventManager.Instance.TriggerEvent(EventStrings.GAME_WIN);
            }
            else
            {
                //没有钥匙
                Debug.Log("No Key");
            }
        }
    }
}
