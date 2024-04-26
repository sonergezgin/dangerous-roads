using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(transform.position.x, transform.position.y, 60) * Time.deltaTime);
    }
}
