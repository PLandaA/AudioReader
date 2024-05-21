using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    public float scrollSpeedX, scrollSpeedY;
    MeshRenderer meshRenderer;

    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();

    }

    void Update () {
        meshRenderer.material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * scrollSpeedX, Time.realtimeSinceStartup * scrollSpeedY);
    }
}
