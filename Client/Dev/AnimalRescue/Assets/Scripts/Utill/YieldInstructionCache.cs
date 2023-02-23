using System.Collections.Generic;
using UnityEngine;

public class YieldInstructionCache
{
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>();
    private static readonly Dictionary<float, WaitForSecondsRealtime> timeIntervalReal = new Dictionary<float, WaitForSecondsRealtime>();


    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;

        if (!timeInterval.TryGetValue(seconds, out wfs))
            timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));

        return wfs;
    }

    public static WaitForSecondsRealtime WaitForSecondsRealTime(float seconds)
    {
        WaitForSecondsRealtime wfsReal;

        if (!timeIntervalReal.TryGetValue(seconds, out wfsReal))
            timeIntervalReal.Add(seconds, wfsReal = new WaitForSecondsRealtime(seconds));

        return wfsReal;
    }
}