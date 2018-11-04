using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    public Text anyKey;
    private bool isPressed = false;
    public Color[] colors;
    private int Length;
    public AudioSource SE;
    public GameObject currentScene;
    public string SceneName = "";


    private void Start()
    {
        Length = colors.Length;
    }
    void Update ()
    {
        if (Input.anyKey && isPressed == false)
        {

            StartCoroutine(wait(0.1f));
            isPressed = true;

        }

        changeColor();

    }
    private int n = 0;
    private void changeColor()
    {
        if ((Mathf.Abs(anyKey.color.r - colors[n].r) > 0.05)&& isPressed == false)
        {
            //TODO: play SFX
            SE.Play();
            anyKey.color = Color.Lerp(anyKey.color, colors[n], Time.deltaTime * 4);
        }

        else
            n++;
        if (n == Length)
            n = 0;
    }

    IEnumerator wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        blink(0f);
        yield return new WaitForSeconds(waitTime);
        blink(1f);
        yield return new WaitForSeconds(waitTime);
        blink(0f);
        yield return new WaitForSeconds(waitTime);
        blink(1f);
        yield return new WaitForSeconds(waitTime);
        blink(0f);
        yield return new WaitForSeconds(waitTime);
        blink(1f);
        yield return new WaitForSeconds(waitTime);
        blink(0f);
        yield return new WaitForSeconds(waitTime);
        //TODO: Change scene
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        Resources.UnloadUnusedAssets();
        Destroy(currentScene);
    }

    private void blink(float alpha)
    {   print("blink");
       
        anyKey.color = new Color(anyKey.color.r, anyKey.color.g, anyKey.color.b, alpha);
        print(anyKey.color);
    }
}
