using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textChanger : MonoBehaviour
{
        
    [SerializeField] private TMP_Text UItext;                                              // UI TEXT 
    [SerializeField] private TMP_Text tabelatext;                                          //Sign Text

    public GameObject shoot;                                                               //referance to access isFiring from shooting
    private  Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        shooting=shoot.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()    
    {
        if (shooting.isFiring)                                                             //update texts 
        {
            UItext.text = "Last Fired bullets:" + shooting.counter;
            tabelatext.text = "Last Fired bullets:" + shooting.counter;
        }
    }
}
