using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    private void Start()
    {
        //StartCoroutine(GetDate());
        //StartCoroutine(Login("",""));
        //StartCoroutine(RegisterUser("",""));
    }

    //public void ShowUserItems()
    //{
    //    StartCoroutine(GetItemsID(Main.instance.userInfo.UserID));
    //}


    IEnumerator GetDate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://phptutorial.hstn.me/GetDate.php"))
        {
            // ---testing aeonfree
            www.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            www.SetRequestHeader("Accept-Encoding", "gzip, deflate");
            www.SetRequestHeader("Accept-Language", "en");
            www.SetRequestHeader("Cache-Control", "max-age=0");
            www.SetRequestHeader("Cookie", "__test=d4f16507ae75e677830d2f5a3f570eca");
            www.SetRequestHeader("Upgrade-Insecure-Requests", "1");
            www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Safari/537.36");
            yield return www.Send();

            string uri = "http://phptutorial.hstn.me/GetDate.php";
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + www.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + www.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityPHPLearning/GetUsers.php"))
        {
            // ---testing aeonfree
            www.SetRequestHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            www.SetRequestHeader("Accept-Encoding", "gzip, deflate");
            www.SetRequestHeader("Accept-Language", "en");
            www.SetRequestHeader("Cache-Control", "max-age=0");
            www.SetRequestHeader("Cookie", "__test=d4f16507ae75e677830d2f5a3f570eca");
            www.SetRequestHeader("Upgrade-Insecure-Requests", "1");
            www.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Safari/537.36");
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("www.error");
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);

                //or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityPHPLearning/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Main.instance.userInfo.SetCredentials(username, password);
                Main.instance.userInfo.SetID(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("Wrong credentials.") || www.downloadHandler.text.Contains("Username does not exist."))
                {
                    Debug.Log("Try again.");
                }
                else
                {
                    //if we logged in correctly

                    Main.instance.userProfile.SetActive(true);
                    Main.instance.login.gameObject.SetActive(false);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityPHPLearning/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemsID(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityPHPLearning/GetItemsID.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("www.error");
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                //Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityPHPLearning/GetItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("www.error");
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                //Call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator SellItem(string ID, string itemID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityPHPLearning/SellItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);

            }
        }
    }
}
