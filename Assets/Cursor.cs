using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject s;

    void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 cusorpo = Camera.main.WorldToViewportPoint(Input.mousePosition);

        s.transform.position = cusorpo;
    }
}
