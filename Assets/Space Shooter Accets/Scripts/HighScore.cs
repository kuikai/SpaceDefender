using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
public class HighScore : MonoBehaviour
{
   
    // Start is called before the first frame update
    private void Start()
    {
        SetUpHowMenyHighScores(6);
    }
    public static void SortHighScore()
    {
        int score;
        List<int> HighScores = new List<int>();

        string line = "";
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("Assets/Space Shooter Accets/HighScore.txt");
            //Read the first line of text
            while (line != null)
            {
                //write the lie to console window
              
                //Read the next line
                line = sr.ReadLine();
                try
                {
                    score = Int32.Parse(line);
                    HighScores.Add(score);          
                }
                catch
                {
                  
                }
                Debug.Log(line);

            }
            sr.Close();

        }
        catch (Exception e)
        {
            Debug.LogError("shit");
        }

        HighScores.Sort();
        HighScores.Reverse();
        Debug.Log(HighScores.Count);

        //clear text file
        try
        {
            StreamWriter sr = new StreamWriter("Assets/Space Shooter Accets/HighScore.txt");

            sr.Flush();

            sr.Close();


        }
        catch (Exception e)
        {
            Debug.LogError("dame");
        }

        for (int i = 0; i < HighScores.Count; i++)
        {

            try
            {
                StreamWriter sr = new StreamWriter("Assets/Space Shooter Accets/HighScore.txt", true, Encoding.ASCII);

                sr.WriteLine(HighScores[i]);

                sr.Close();

            }
            catch (Exception e)
            {
                Debug.LogError("dame");
            }
        }
    }

    public static void AddScore(int score)
    {
       
        string NewHigeScore = score.ToString();

        try
        {
            StreamWriter sr = new StreamWriter("Assets/Space Shooter Accets/HighScore.txt",true, Encoding.ASCII);
        
            sr.WriteLine(NewHigeScore);

            sr.Close();

        }
        catch (Exception e)
        {
            Debug.LogError("dame");
        }


        SortHighScore();
        SetUpHowMenyHighScores(10);

    }

    public static void SetUpHowMenyHighScores(int numberOfScores)
    {
        int score;
        List<int> HighScores = new List<int>();

        string line = "";
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("Assets/Space Shooter Accets/HighScore.txt");
            //Read the first line of text
            while (line != null)
            {
                //write the lie to console window

                //Read the next line
                line = sr.ReadLine();
                try
                {
                    score = Int32.Parse(line);
                    HighScores.Add(score);
                }
                catch
                {

                }
            //    Debug.Log(line);

            }
            sr.Close();

        }
        catch (Exception e)
        {
            Debug.LogError("shit");
        }

        if (numberOfScores < HighScores.Count)
        {
            Debug.Log(HighScores.Count);
            HighScores.RemoveAt(HighScores.Count - 1);
        }
      

        Debug.Log("lol");

        //clear text file
        try
        {
            StreamWriter sr = new StreamWriter("Assets/Space Shooter Accets/HighScore.txt");

            sr.Flush();

            sr.Close();


        }
        catch (Exception e)
        {
            Debug.LogError("dame");
        }

        
        for (int i = 0; i < HighScores.Count; i++)
        {

            try
            {
                StreamWriter sr = new StreamWriter("Assets/Space Shooter Accets/HighScore.txt", true, Encoding.ASCII);

                sr.WriteLine(HighScores[i]);

                sr.Close();

            }
            catch (Exception e)
            {
                Debug.LogError("dame");
            }
        }
    }

    public static List<int> GetHighScore()
    {

        int score;
        List<int> HighScores = new List<int>();

        string line = "";
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("Assets/Space Shooter Accets/HighScore.txt");
            //Read the first line of text
            while (line != null)
            {
                //write the lie to console window

                //Read the next line
                line = sr.ReadLine();
                try
                {
                    score = Int32.Parse(line);
                    HighScores.Add(score);
                }
                catch
                {

                }
                //    Debug.Log(line);

            }
            sr.Close();

        }
        catch (Exception e)
        {
            Debug.LogError("shit");
        }

        return HighScores;
    }





}
