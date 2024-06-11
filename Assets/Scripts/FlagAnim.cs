using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnim : MonoBehaviour
{
    Vector3 initPos;
    public float freq;
    public float amp;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, 0);
    }
}
