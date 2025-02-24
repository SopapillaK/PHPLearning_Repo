using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class Items : MonoBehaviour
{
    Action<string> _createItemsCallback;
    // Start is called before the first frame update
    void Start()
    {
        _createItemsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.instance.userInfo.UserID;
        StartCoroutine(Main.instance.web.GetItemsID(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //parsing json array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++) 
        {
            //create local vars
            bool isDone = false; //are we done downloading
            string itemId = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            //create callback to get info from Web.cs
            Action<string> getItemmInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.instance.web.GetItem(itemId, getItemmInfoCallback));

            //wait until callback is called from web (info finished downloading)
            yield return new WaitUntil(() => isDone == true);

            //insatiate gameobject (item prefab)
            GameObject item = Instantiate(Resources.Load("Prefab/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            //fill info
            item.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

            //continue to next item
        }
        yield return null;
    }
}
