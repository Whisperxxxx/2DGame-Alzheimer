using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bases;
    [SerializeField]
    private GameObject[] maps;
    [SerializeField]
    private GameObject[] backgrounds;
    [SerializeField]
    private GameObject[] elevators;
    [SerializeField]
    private GameObject[] doors;
    [SerializeField]
    private GameObject[] balloons;
    [SerializeField]
    private GameObject[] nerveCells;
    [SerializeField]
    private GameObject[] hazards;
    [SerializeField]
    private GameObject[] keys;

    public AudioSource switchAudio, beforeAudio, afterAudio;
    public float switchThreshold = 0.6f; // 控制何时切换地图的时间百分比

    private int currentIndex = 0; // 当前地图索引
    private bool switchTriggered = false; // 用于跟踪是否已经触发过地图切换

    void Start()
    {
        ActivateMap(0); // 激活第一个地图
        beforeAudio.Play();
    }

    void Update()
    {
        if (GameUIManager.Instance != null && ShouldSwitchMap() && !switchTriggered)
        {
            SwitchMaps();
            switchTriggered = true; // 标记为已触发切换
        }
        else if (GameUIManager.Instance.timeCountDown > GameUIManager.Instance.timeMax * switchThreshold)
        {
            switchTriggered = false; // 重置触发状态
        }
    }

    private bool ShouldSwitchMap()
    {
        return GameUIManager.Instance.timeCountDown <= GameUIManager.Instance.timeMax * switchThreshold;
    }

    private void SwitchMaps()
    {
        int nextIndex = (currentIndex + 1) % maps.Length; // 循环切换
        ActivateMap(nextIndex);

        beforeAudio.Stop();
        switchAudio.Play();
        afterAudio.Play();
        YTEventManager.Instance.TriggerEvent(EventStrings.AMNESIC_POINT);

        currentIndex = nextIndex;
    }

    private void ActivateMap(int index)
    {
        SetActiveForGroup(bases, index);
        SetActiveForGroup(maps, index);
        SetActiveForGroup(backgrounds, index);
        SetActiveForGroup(elevators, index);
        SetActiveForGroup(doors, index);
        SetActiveForGroup(balloons, index);
        SetActiveForGroup(nerveCells, index);
        SetActiveForGroup(hazards, index);
        SetActiveForGroup(keys, index);
    }

    private void SetActiveForGroup(GameObject[] group, int index)
    {
        if (group.Length > 1)
        {
            for (int i = 0; i < group.Length; i++)
            {
                group[i].SetActive(i == index);
            }
        }
        else if (group.Length == 1)
        {
            group[0].SetActive(true); // 如果只有一个对象，始终保持激活状态
        }
    }
}
