using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    CapsuleCollider col;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }

    public void TeleportTo(Vector3 feetPos)
    {
        transform.position = feetPos + new Vector3(0, transform.localScale.y * col.height * .5f, 0);
    }

    float speedHorizontal = 3.0f;
    Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Vertical") * transform.forward;
        dir += Input.GetAxisRaw("Horizontal") * transform.right;
        transform.Translate(dir * Time.deltaTime * speedHorizontal);
    }
}
