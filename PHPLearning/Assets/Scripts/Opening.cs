using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    //public Button loginButton;
    //public Button registerButton;
    public GameObject loginScreen;
    public GameObject registerScreen;
    
    public void Login()
    {
        loginScreen.SetActive(true);
        registerScreen.SetActive(false);
    }

    public void Register()
    {
        registerScreen.SetActive(true);
        loginScreen.SetActive(false);
    }

}
