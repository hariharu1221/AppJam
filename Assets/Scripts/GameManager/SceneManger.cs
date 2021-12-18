using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManger : MonoBehaviour
{
    public static SceneManger instance;

    public GameObject Setin;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

       // Setin = GetComponent<GameObject>();
    }
    public void GotoIngame()
    {
        SceneManager.LoadScene("InGameScene");
    }
  public void GotoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void SetUp()
    {
        Setin.SetActive(true);
    }
    public void SetUpX()
    {
        Setin.SetActive(false);
    }
    
}
