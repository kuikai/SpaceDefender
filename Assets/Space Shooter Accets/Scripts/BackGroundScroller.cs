using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundsScrolleSpeed = 0.5f;
    Material mymaterial;
    Vector2 offset;
    void Start()
    {
        mymaterial = GetComponent<Renderer>().material;

        offset = new Vector2(0, backgroundsScrolleSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        mymaterial.mainTextureOffset += offset * Time.deltaTime;        
    }
}
