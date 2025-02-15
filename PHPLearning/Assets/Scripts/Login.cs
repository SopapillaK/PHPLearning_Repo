using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.instance.web.Login(usernameInput.text, passwordInput.text));
        });
    }

}
