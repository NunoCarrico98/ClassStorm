using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(bool value, Transform target = null)
    {
        if (value)
        {
            transform.position = target.position;

            Vector3 targetPostition = new Vector3(transform.position.x,
                                       cam.position.y,
                                       cam.position.z);
            transform.LookAt(targetPostition);
        }
        spriteRenderer.enabled = value;
    }
}
