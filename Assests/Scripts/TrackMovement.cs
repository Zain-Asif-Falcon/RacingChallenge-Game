using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TrackMovement : MonoBehaviour
{
    public Renderer TrackRenderer;
    private float TrackMovementSpeed = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MoveTrack());
    }

    // Update is called once per frame
    void Update()
    {
        TrackRenderer.material.mainTextureOffset += new Vector2(0, TrackMovementSpeed * Time.deltaTime);
        //TrackRenderer.material.mainTextureOffset += new Vector2(0, TrackMovementSpeed * Time.deltaTime);
    }
    IEnumerator MoveTrack()
    {
        while (true)
        {
            TrackRenderer.material.mainTextureOffset += new Vector2(0, TrackMovementSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
