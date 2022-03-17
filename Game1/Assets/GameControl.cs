using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject cueBall;
    Rigidbody cueBallRigidBody;
    public float flickSize;
    public GameObject cue;
    Transform cueTransform;
    public float rotationSpeed;
    public GameObject protoTarget;
    List<GameObject> targets;
    public int numberOfTargets;
    bool done;
    int targetsCollected;
    public Text collected, time;
    float currentTimeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        cueBallRigidBody = cueBall.GetComponent<Rigidbody>();
        cueTransform = cue.GetComponent<Transform>();
        targets = new List<GameObject>();
        SetUpTargets(numberOfTargets);
        collected.text = "Collected so far " + targetsCollected;
        done = false;
        currentTimeElapsed = 0;
    }

    void SetUpTargets(int nTargets)
    {
        int startNumber = targets.Count;
        for(int i = startNumber; i< nTargets; i++)
        {
            GameObject target = GameObject.Instantiate(protoTarget);
            SetTargetLocation(target);
            targets.Add(target);
        }
    }
    void SetTargetLocation(GameObject g)
    {
        Transform t = g.transform;
        Vector3 pos = t.position;
        float x = Random.Range(-4f, 4f);
        float z = Random.Range(-4f, 4f);
        pos.x = x;
        pos.z = z;
        t.position = pos;
    }

    void UpdateCue()
    {
        Vector3 mPosition = Input.mousePosition;
        float xPos = 20 * ((mPosition.x / 1920f) - 0.5f);
        float zPos = 10 * ((mPosition.y / 1080f) - 0.5f);
        //Debug.Log(xPos + " " + zPos);
        Vector3 pos = new Vector3(xPos, 1f, zPos);
        cueTransform.position = pos;
        float h = Input.GetAxis("Horizontal");

        if (Mathf.Abs(h) > 0)
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

    int CountScore()
    {
        int count = 0;
        foreach (GameObject g in targets)
        {
            if (!g.activeSelf)
            {
                count++;
            }
        }
        return count;
    }
    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            UpdateCue();
            targetsCollected = CountScore();  
            collected.text = "Collected so far " + targetsCollected;
            currentTimeElapsed += Time.deltaTime;
            time.text = "Time " + currentTimeElapsed;
            if (targetsCollected == numberOfTargets)
            {
                done = true;
            }
        }
        
    }
}
