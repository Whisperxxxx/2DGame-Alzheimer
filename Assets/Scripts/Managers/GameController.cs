using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject map1;
    [SerializeField]
    GameObject map2;
    [SerializeField]

    void Start()
    {
        // Only active the first map when the game starts
        map1.SetActive(true); 
        map2.SetActive(false);

    }

    void Update()
    {
 
        if (GameUIManager.Instance != null)
        {
            if (GameUIManager.Instance.timeCountDown <= GameUIManager.Instance.timeMax * 0.6)
            {
                SwitchMaps();
            }
        }
    }

    private void SwitchMaps()
    {
        // change the maps
        if (map1.activeSelf)
        {
            map1.SetActive(false);
            map2.SetActive(true);
        }
    }
}
