using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void start_function()
    {
        SceneManager.LoadScene(1);
    }
    public void exit_function()
    {
        SceneManager.LoadScene(2);
    }
    public void sure_yes_function()
    {
        Application.Quit();
    }
    public void sure_no_function()
    {
        SceneManager.LoadScene(0);
    }
    public void return_function()
    {
        SceneManager.LoadScene(0);
    }
}
