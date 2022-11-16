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

        foreach (var meshRenderer in meshRenderers)
        {
            var rand = Random.Range(0, textures.Count);
            meshRenderer.material.mainTexture = textures[rand];
        }
    }
}
