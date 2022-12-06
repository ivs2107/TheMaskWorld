using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PreserveSizeChildren : MonoBehaviour
{
    public float FixeScale = 1;
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        //to preserve Size of children
        transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);

    }
}
