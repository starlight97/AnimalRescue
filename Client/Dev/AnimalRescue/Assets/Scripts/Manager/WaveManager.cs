using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    private int currentWave;
    public float waveTime;
    public UnityAction<int> onWaveStart;

    public void Init()
    {
        currentWave = 0;
        StartWave();
    }

    // 보스테스트용 TestInit
    public void TestInit()
    {
        currentWave = 4;
        StartWave();
    }

    public void StartWave()
    {
        StartCoroutine(this.StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine()
    {
        while (true)
        {
            currentWave++;
            onWaveStart(currentWave);
            if (currentWave % 5 == 0)
                break;
            yield return new WaitForSeconds(waveTime);
        }
    }
}
