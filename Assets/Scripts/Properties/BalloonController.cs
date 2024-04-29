using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public Vector3 initialPos;
    public Vector3 finalPos;
    
    public float moveDuration = 3.0f; // 移动到目标位置所需的时间  
    private Rigidbody2D rb; // Rigidbody2D组件引用  

    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        finalPos=transform.parent.position;
        
        rb = PlayerController.Instance.GetComponent<Rigidbody2D>(); // 获取Rigidbody2D组件  
        
        audioSource= GetComponent<AudioSource>();
    }
    
    //当碰撞到玩家时
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!PlayerController.Instance.isOld && other.CompareTag("Player"))
        {
            // 开始移动到目标位置  
            audioSource.Play();
            StartCoroutine(MoveToTargetPosition());  
        }
    }
    
    IEnumerator MoveToTargetPosition()  
    {  
        float elapsedTime = 0f; // 已经过去的时间  
  
        while (elapsedTime < moveDuration)  
        {  
            elapsedTime += Time.deltaTime; // 更新已经过去的时间  
            float t = elapsedTime / moveDuration; // 计算插值参数  
  
            // 使用Lerp平滑地插值到目标位置  
            transform.position = Vector3.Lerp(initialPos, finalPos, t);
            PlayerController.Instance.transform.position = Vector3.Lerp(initialPos, finalPos, t);  
  
            yield return null; // 等待下一帧  
        }  
  
        // 到达目标位置后，确保位置精确匹配  
        transform.position = initialPos;
        PlayerController.Instance.transform.position = finalPos;  
    }  
}
