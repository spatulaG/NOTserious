using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeHead : MonoBehaviour
{
    public GameObject Profile;
    public Sprite[] HeadImages;

    private GameObject hero;
    public void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
    }

    public void setImage()
    {
        int heroLife = hero.GetComponent<Hero>().getHP();

<<<<<<< HEAD
        
        Profile.GetComponent<SpriteRenderer>().sprite = HeadImages[heroLife-1 >= 0 ? heroLife-1 : 0];
=======
        if(heroLife > 0)
            Profile.GetComponent<SpriteRenderer>().sprite = HeadImages[heroLife-1];
>>>>>>> master
        
    }

    private void Update()
    {
        setImage();
    }

}
