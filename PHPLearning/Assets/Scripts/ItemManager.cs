using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class ItemManager : MonoBehaviour
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
            string id = jsonArray[i].AsObject["ID"];

            JSONObject itemInfoJson = new JSONObject();

            //create callback to get info from Web.cs
            Action<string> getItemmInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0] as JSONObject;
            };

            StartCoroutine(Main.instance.web.GetItem(itemId, getItemmInfoCallback));

            //wait until callback is called from web (info finished downloading)
            yield return new WaitUntil(() => isDone == true);

            //insatiate gameobject (item prefab)
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Item item = itemGo.AddComponent<Item>();

            item.ID = id;
            item.itemID = itemId;

            itemGo.transform.SetParent(this.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;

            //fill info
            itemGo.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            itemGo.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            itemGo.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

            //set sell button
            itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => {
                string idInInventory = id;
                string userId = Main.instance.userInfo.UserID;
                StartCoroutine(Main.instance.web.SellItem(idInInventory, userId));
            });

            //continue to next item
        }
        yield return null;
    }
}
