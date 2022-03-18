using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class OpenSceneScript : MonoBehaviour
{
    TextReader readUserData;
    TextWriter writeUserData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnNewUserClick()
    {
        SceneManager.LoadScene("UseerDetails");
    }
    public void OnCurrentUser()
    {
        try
        {
            readUserData = new StreamReader("userdata.txt");
            string lineRead = readUserData.ReadLine();
            writeUserData = new StreamWriter("currentUser.txt");
            string[] parts = lineRead.Split(',');
            writeUserData.WriteLine(parts[1]);
            lineRead = readUserData.ReadLine();
            parts = lineRead.Split(',');
            writeUserData.WriteLine(parts[1]);
            int i = System.Int32.Parse(parts[1]);
            for(int j = 0; j < i; j++)
            {
                lineRead = readUserData.ReadLine();
                parts = lineRead.Split(',');
                writeUserData.WriteLine(parts[1]);
            }
            readUserData.Close();
            writeUserData.Close();
            SceneManager.LoadScene("BallFlick");
        }
        catch (FileNotFoundException fne)
        {
            SceneManager.LoadScene("UseerDetails");
        }
    }
    public void OnQuitButton()
    {
        Application.Quit();
        //in Editor mode this will not run
    }

}
