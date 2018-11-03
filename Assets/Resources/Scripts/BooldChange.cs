using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BooldChange : MonoBehaviour
{
    public Image ColorIamge;
    public GameObject ColorWheelPanel;
    public Image BooldImage;
    public GameObject BooldPanel;

    private GameObject hero;
    public void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        showColorWheelAndBoold();
        /*
        ColorIamge.gameObject.SetActive(false);
        ColorWheelPanel.SetActive(false);
        BooldImage.gameObject.SetActive(false);
        BooldPanel.gameObject.SetActive(false);*/
    }
    public void showColorWheelAndBoold()
    {
        ColorIamge.gameObject.SetActive(true);
        ColorWheelPanel.SetActive(true);
        BooldImage.gameObject.SetActive(true);
        BooldPanel.SetActive(true);
    }

    public void setBooldImage()
    {
        if(hero.GetComponent<Hero>().getHP() == 1)
        {
            Sprite EmptyBoold = Resources.Load("Material/EmptyBlood", typeof(Sprite)) as Sprite;
            BooldImage.GetComponent<Image>().sprite = EmptyBoold;
        }
        else if (hero.GetComponent<Hero>().getHP() == 2)
        {
            Sprite HalfBoold = Resources.Load("Material/HalfBlood", typeof(Sprite)) as Sprite;
            BooldImage.GetComponent<Image>().sprite = HalfBoold;
        }
        else if (hero.GetComponent<Hero>().getHP() == 3)
        {
            Sprite FullBoold = Resources.Load("Material/FullBlood",typeof(Sprite)) as Sprite;
            BooldImage.GetComponent<Image>().sprite = FullBoold;
        }
    }

    private void Update()
    {
        setBooldImage();
    }

}
