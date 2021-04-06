using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthDisplay : MonoBehaviour
{
    // Start is called before the first frame update

  [SerializeField]  TextMeshProUGUI Health;
    void Start()
    {
        if (FindObjectOfType<Player>() != null)
        {
            Health.text = FindObjectOfType<Player>().Gethealth().ToString();
        }
    }


    public void Setheath()
    {
        Health.text = FindObjectOfType<Player>().Gethealth().ToString();
    }
    public void SetHealth(int Hp)
    {
        Health.text = Hp.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
