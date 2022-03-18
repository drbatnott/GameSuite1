using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour
{
    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        string n = collision.collider.gameObject.name;
        switch (n)
        {
            case "Table":
                break;
            case "RightWall":
                Debug.Log(t.forward);
                break;
            default:
                break;

        }
    }
}
