using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
    public LayerMask whatToHit;
    public GameObject lineEffectPrefab;

    public float shootRatio;
    private float time;
    private AudioSource audio;
    public float damage;
    public int ammunition;
    public Text ammotext;
	// Use this for initialization
	void Start () {
        this.UpdateAmmoText();
        this.time = 0;
        this.audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        this.time += Time.deltaTime;

        if (InputManager.current.GetShooting() && (this.time > this.shootRatio))
        {

            this.time = 0;

            if (this.ammunition > 0)
            {

            
            ////////////////////////////funcion
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.right, 10, whatToHit);
            GameObject tmp = Instantiate(this.lineEffectPrefab) as GameObject;
            LineRenderer line = tmp.GetComponent<LineRenderer>();
            if (hit.collider)
            {
                line.SetPosition(0, this.transform.position);
                line.SetPosition(1, hit.point);
                Debug.Log("choca");
                ///////////////////enemy////////////////////
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.SendMessage("Hurt", this.damage);
                }

                    if (hit.collider.gameObject.CompareTag("Boss"))
                    {
                        hit.collider.gameObject.SendMessage("Hurt", this.damage);
                    }
                }
            else
            {
                line.SetPosition(0, this.transform.position);
                line.SetPosition(1, this.transform.position + (this.transform.right * 10));
                Debug.Log("no choca");
            }
            Destroy(tmp, 0.5f);
            ////////////////7777777777777777777777
            this.audio.Play();
            this.ammunition--;
            this.UpdateAmmoText();

        }
           
        }	


	}

    void UpdateAmmoText()
    {
        this.ammotext.text = "Municion: " + this.ammunition;

    }

    public void FoodAmmo() {

        this.ammunition += 10;
        this.UpdateAmmoText();
    }
    
}
