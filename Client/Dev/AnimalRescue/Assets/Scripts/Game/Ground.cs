using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public List<Texture> textures;

    void Start()
    {
        this.meshRenderer = this.transform.GetComponentInChildren<MeshRenderer>();

        var rand = Random.Range(0, textures.Count);
        this.meshRenderer.material.mainTexture = textures[rand];
    }
}
