using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float hp;
    public Slider healthbar;
    public GameObject metalExplotion;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void Hurt(float damage)
    {
        this.hp -= damage;

        if (this.healthbar)
        {
            this.healthbar.value = this.hp;
        }


        if (this.hp <= 0)
        {
            this.hp=0;
           
            GameObject enemyEx =  Instantiate(this.metalExplotion,this.transform.position,Quaternion.identity) as GameObject;

            Destroy(enemyEx, 0.5f);

            
            
            this.gameObject.SendMessage("OnGetKill");
            Destroy(this.gameObject);
            
        }

    }
}
