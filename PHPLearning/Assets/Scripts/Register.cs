using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPassInput;
    public Button createNewButton;
    public GameObject incorrectPass;

    // Start is called before the first frame update
    void Start()
    {
        createNewButton.onClick.AddListener(() =>
        {
            if (passwordInput.text == confirmPassInput.text)
            {
                incorrectPass.SetActive(false);
                StartCoroutine(Main.instance.web.RegisterUser(usernameInput.text, passwordInput.text));
            }
            else
            {
                incorrectPass.SetActive(true);
            }
        });
    }
}

