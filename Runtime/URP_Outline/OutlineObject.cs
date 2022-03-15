using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    [SerializeField] string outlineLayer = "Outline";
    public string OutlineLayer
    {
        get { return outlineLayer; }
        set { outlineLayer = value; }
    }

    int startLayer;

    [SerializeField] Renderer [] meshRenderers;
    public Renderer[] MeshRenderers
    {
        get { return meshRenderers; }
        set { meshRenderers = value; }
    }

    private void Awake()
    {
        startLayer = gameObject.layer;
        MeshRenderers = gameObject.GetComponentsInChildren<Renderer>();
    }
    public void OnEnable()
    {
        int id = LayerMask.NameToLayer(OutlineLayer);

        for (int rendererID = 0; rendererID < meshRenderers.Length; rendererID++)
        {
            var renderer = meshRenderers[rendererID];
            renderer.gameObject.layer = id;
        }
    }

    public void OnDisable()
    {
        int id = startLayer;

        for (int rendererID = 0; rendererID < meshRenderers.Length; rendererID++)
        {
            var renderer = meshRenderers[rendererID];
            renderer.gameObject.layer = id;
        }
    }
}
