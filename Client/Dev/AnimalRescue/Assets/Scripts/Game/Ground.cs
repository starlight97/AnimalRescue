using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer[] meshRenderers;
    public List<Texture> textures;

    void Start()
    {
        this.meshRenderers = this.transform.GetComponentsInChildren<MeshRenderer>();
        var rand = Random.Range(0, textures.Count);
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.material.mainTexture = textures[rand];
        }
    }
}
