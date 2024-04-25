using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myd.Platform
{
    public class ElevatorController : MonoBehaviour
    {
        public float Speed=1f;
        private float TopY, BottomY;
        public float upHeight=3f;
        
        //电梯到达最高点等待时间
        public float waitTimeMax = 2f;
        public float waitTimeLeft;
        public bool isUp = true;
        
        public bool activate = false;

        public bool _playerOn;
        public bool playerOn
        {
            get => _playerOn;
            set
            {
                //人物上电梯时，电梯激活
                if (_playerOn == false && value == true)
                {
                    activate=true;
                }
                //人物在电梯上，电梯不下降
                if (_playerOn == true && value == true&&isUp==false)
                {
                    isUp = true;
                }
                _playerOn = value;
            }
        }
        
        public Bounds initialDetectBounds;
        public Vector3 initialPos=Vector3.zero;
        public Bounds detectBounds
        {
            get
            {
                if (initialPos==Vector3.zero)
                {
                    return initialDetectBounds;
                }
                return new Bounds(initialDetectBounds.center+(transform.position-initialPos), initialDetectBounds.size);
            }
        }

        //脚本附件的物体背选择时调用，仅在编辑器中可见，游戏运行时不会调用
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(detectBounds.center, detectBounds.size);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(detectBounds.center+Vector3.up*upHeight, detectBounds.size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position+Vector3.up*upHeight, 0.5f);
        }

        // Start is called before the first frame update
        void Start()
        {
            TopY = transform.position.y + upHeight; //获取top点
            BottomY = transform.position.y; //获取bottom点
            initialPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (activate)
            {
                Movement();
            }
            if (detectBounds.Contains(PlayerController.Instance.transform.position))
            {
                playerOn = true;
            }
            else
            {
                playerOn = false;
            }
        }

        void Movement()
        {
            if (isUp)
            {
                if (transform.position.y < TopY)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
                }
                if (transform.position.y >= TopY)
                {
                    isUp = false;
                    waitTimeLeft = waitTimeMax;
                }
            }
            else
            {
                //如果激活，就不会下降
                if(waitTimeLeft>0)
                {
                    if(!playerOn) waitTimeLeft -= Time.deltaTime;
                }
                else
                {
                    if (!playerOn)
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
                        if (transform.position.y <= BottomY)
                        {
                            isUp = true;
                            activate = false;
                        }
                    }
                }
            }
        }
    }
}