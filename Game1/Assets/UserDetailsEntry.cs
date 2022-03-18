using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserDetailsEntry : MonoBehaviour
{
    
    TextWriter writeUserData;
    //public Text scoreText;
    public InputField userNameField;
    public GameObject openGameButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInputFieldChanged()
    {
        openGameButton.SetActive(true);
    }
    public void OpenButtonClick()
    {
        writeUserData = new StreamWriter("userdata.txt");
        writeUserData.WriteLine("name,"+userNameField.text);
        writeUserData.WriteLine("levels," + 1);
        writeUserData.WriteLine(1 + "," + 9999);
        writeUserData.Close();
        writeUserData = new StreamWriter("currentUser.txt");
        writeUserData.WriteLine(userNameField.text);
        writeUserData.WriteLine(1);
        writeUserData.WriteLine(9999);
        writeUserData.Close();
        SceneManager.LoadScene("BallFlick");
    }
}
