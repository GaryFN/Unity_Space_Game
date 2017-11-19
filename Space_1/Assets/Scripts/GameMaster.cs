using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    public static GameMaster current;
    private int puntuation;
    public GameObject player;
    private Vector3 startPosition;
    public Text puntuationText;

    public GameObject deathPanel;
    public Text puntDeathText;
    static GameMaster data;
    // Use this for initialization

    private void Awake()
    {
        GameMaster.current = this;
    }

    void Start () {
       

            
            
            this.puntuation = 0;
            this.startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        this.UpdatePuntText();
     
           
        
    }
	
	// Update is called once per frame
	void Update () {
      
    }
    void UpdatePuntText()
    {
        this.puntuationText.text = "- " + this.puntuation + " -";

    }

    public void AddPuntuation(int amount)
    {
        this.puntuation += amount;
        this.UpdatePuntText();
    }


    public void GameOver()
    {
        this.deathPanel.SetActive(true);
        this.puntDeathText.text = "Score: " + this.puntuation;
    }

    public void ReloadScene()
    {
        Debug.Log("entro al reinicio");
        Application.LoadLevel(0);
       

    }
    public void ReloadScene1()
    {
        Debug.Log("entro al reinicio");
        Application.LoadLevel(0);


    }
    public void ExitMenu()
    {
        Debug.Log("entro al reinicio");
        Application.Quit();


    }


}
