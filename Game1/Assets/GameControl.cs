using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject cueBall;
    Rigidbody cueBallRigidBody;
    public float flickSize;
    public GameObject cue;
    Transform cueTransform;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cueBallRigidBody = cueBall.GetComponent<Rigidbody>();
        cueTransform = cue.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPosition = Input.mousePosition;
        float xPos = 20 * ((mPosition.x/1920f) -0.5f);
        float zPos = 10*((mPosition.y/1080f)- 0.5f);
        //Debug.Log(xPos + " " + zPos);
        Vector3 pos = new Vector3(xPos, 1f, zPos);
        cueTransform.position = pos;
        float h = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(h)>0)
        {
            cueTransform.RotateAround(pos, Vector3.up, rotationSpeed * h * Time.deltaTime);
           // Debug.Log(cueTransform.rotation.eulerAngles.x);
        }
        Vector3 flickD = -cueTransform.forward;
        if (Input.GetButton("Jump"))
        {
            Vector3 flick = new Vector3(flickD.x, 0, flickD.z);
            cueBallRigidBody.AddForce(flickSize * flick, ForceMode.Impulse);
        }
    }
}
