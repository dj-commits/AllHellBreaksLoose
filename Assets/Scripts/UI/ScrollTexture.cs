using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{

    [SerializeField]
    float scrollSpeed;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveTexture = Time.time * scrollSpeed;
        spriteRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, moveTexture));
    }
}
