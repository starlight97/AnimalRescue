using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    private int currentWave = 1;
    public float waveTime;
    public UnityAction<int> onWaveStart;

    public void Init()
    {
        StartCoroutine(this.StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine()
    {
        while (true)
        {
            onWaveStart(currentWave++);
            yield return new WaitForSeconds(waveTime);
        }
    }
}
