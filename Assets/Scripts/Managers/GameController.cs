using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject base1;
    [SerializeField]
    GameObject base2;
    [SerializeField]
    GameObject map1;
    [SerializeField]
    GameObject map2;
 
    public AudioSource switchAudio, beforeAudio, afterAudio;

    void Start()
    {
        // Only active the first map when the game starts
        base1.SetActive(true);
        base2.SetActive(false);
        map1.SetActive(true); 
        map2.SetActive(false);
        beforeAudio.Play();

    }

    void Update()
    {
 
        if (GameUIManager.Instance != null)
        {
            if (GameUIManager.Instance.timeCountDown <= GameUIManager.Instance.timeMax * 0.6)
            {
                //逆天代码
                if (map1.activeSelf)
                {
                    YTEventManager.Instance.TriggerEvent(EventStrings.AMNESIC_POINT);
                }
                SwitchMaps();
            }
        }
    }

    private void SwitchMaps()
    {

        // change the maps
        if (map1.activeSelf)
        {
            beforeAudio.Stop();
            switchAudio.Play();
            afterAudio.Play();
            base1.SetActive(false);
            base2.SetActive(true);
            map1.SetActive(false);
            map2.SetActive(true);
        }
    }
}
