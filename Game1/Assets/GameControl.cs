using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

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
    public Text collected, time, levelText, levelBestTimeText,timeThisGo;
    public GameObject resultPanel, nextLevelButton, repeatLevelButton;
    int level;
    float [] bestTime;
    float currentTimeElapsed;
    public int levelsAvailable;
    public int targetIncrement;
    // Start is called before the first frame update
    void Start()
    {
       // TextWriter tw = new StreamWriter("Data.txt");
        
        resultPanel.SetActive(false);
        cueBallRigidBody = cueBall.GetComponent<Rigidbody>();
        cueTransform = cue.GetComponent<Transform>();
        targets = new List<GameObject>();
        SetUpTargets(numberOfTargets);
        collected.text = "Collected so far " + targetsCollected;
        done = false;
        currentTimeElapsed = 0;
        level = 1;
        levelText.text = "Level " + level;
        bestTime = new float[levelsAvailable];
        for(int i = 0; i < levelsAvailable; i++)
        {
            bestTime[i] = 999999f;
            //tw.WriteLine("Level," + i + "," + bestTime[i]);
        }
        //tw.Close();
        levelBestTimeText.text = "Level Best Time " + String.Format("{0:0.0}", bestTime[0]);
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
        float x = UnityEngine.Random.Range(-4f, 4f);
        float z = UnityEngine.Random.Range(-3f, 3f);
        pos.x = x;
        pos.z = z;
        pos.y = 0.25f;
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
            time.text = "Time " + String.Format("{0:0.0}", currentTimeElapsed);
            if (targetsCollected == numberOfTargets)
            {
                done = true;
                timeThisGo.text = "Time\n" + String.Format("{0:0.0}", currentTimeElapsed);
                if (currentTimeElapsed < bestTime[level - 1])
                {
                    bestTime[level - 1] = currentTimeElapsed;
                    timeThisGo.text += "\nYou have a new best Time!";
                    levelBestTimeText.text = "Level Best Time " + String.Format("{0:0.0}", currentTimeElapsed);
                }
                if(level < levelsAvailable)
                {
                    nextLevelButton.SetActive(true);

                }
                else
                {
                    nextLevelButton.SetActive(false);
                }
                resultPanel.SetActive(true);
            }
        }   
    }

    void ResetValues()
    {
        currentTimeElapsed = 0;
        resultPanel.SetActive(false);
        targetsCollected = 0;
        foreach (GameObject g in targets)
        {
            g.SetActive(true);
            SetTargetLocation(g);
        }
        levelText.text = "Level " + level;
        done = false;
        cueBall.transform.position = new Vector3(0f, 0.352f, 0f);
        cueBallRigidBody.velocity = Vector3.zero;
    }

    public void OnRepeatLevel()
    {
        ResetValues();
        levelBestTimeText.text = "Level Best Time " + String.Format("{0:0.0}", bestTime[level-1]);
    }
    public void OnNextLevel()
    {
        level++;
        //Debug.Log(level);
        ResetValues();
        numberOfTargets += targetIncrement;
        
        //Debug.Log(targets.Count);
        for(int i = targets.Count; i < numberOfTargets; i++)
        {
            GameObject newTarget = GameObject.Instantiate(protoTarget);
            SetTargetLocation(newTarget);
            targets.Add(newTarget);
        }       
        levelBestTimeText.text = "Level Best Time " + String.Format("{0:0.0}", bestTime[level-1]);
    }
}
