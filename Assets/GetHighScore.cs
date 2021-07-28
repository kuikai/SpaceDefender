using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetHighScore : MonoBehaviour
{

    TextMeshProUGUI Text;

    [SerializeField] TextMeshProUGUI NumberText;
    List<int> HigeScoes = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();

        Text.text = "";
        NumberText.text = "";
        HigeScoes = HighScore.GetHighScore();

        for (int i = 0; i < HigeScoes.Count; i++)
        {

            Text.text += HigeScoes[i].ToString() + "\n";
            int number = i + 1;
            NumberText.text += number.ToString()+"." + "\n"; 
        }
        
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
