using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private float height;

    private GameObject stoneGo;

    public void Init(GameObject stoneGo)
    {
        this.stoneGo = stoneGo;
        this.height = this.stoneGo.transform.position.y;
    }

    void Update()
    {
        if (this.stoneGo != null) 
        {
            var distance = Vector3.Distance(this.transform.position, this.stoneGo.transform.position);
            distance *= 5;
            // distance : 2 ~ 0.3 
            var scale = Mathf.Clamp(distance / this.height, 1f, 5f);
            this.transform.localScale = new Vector3(scale, scale, scale);
        }
        
    }
}
