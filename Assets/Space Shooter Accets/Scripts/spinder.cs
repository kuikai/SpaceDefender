using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinder : MonoBehaviour
{
    [SerializeField] float speedspine;
    void Update()
    {
        transform.Rotate(0, 0, speedspine * Time.deltaTime);
    }
}
