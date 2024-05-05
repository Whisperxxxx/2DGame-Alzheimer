using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private Vector3 initialPos;
    public AudioSource audioSource;
    private void Start()
    {
        initialPos=transform.position;
        YTEventManager.Instance.AddEventListener(EventStrings.AMNESIC_POINT, LossKey);
        
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            PlayerController.Instance.haveKey = true;
            StartCoroutine(DisableAfterSound());
        }
    }

    private IEnumerator DisableAfterSound()
    {
        // 计算等待时间，确保不会产生负数的等待时间
        float waitTime = Mathf.Max(0, audioSource.clip.length - 0.3f);
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
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
