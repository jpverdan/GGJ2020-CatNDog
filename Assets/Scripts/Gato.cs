using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gato : MonoBehaviour
{
    private List<Item> _listOfItems = new List<Item>();
    public float timeToBreakItemInSeconds = 5f;
    private float _coolDownToBreakItem;

    // Start is called before the first frame update
    void Start()
    {
        GetAllItems();
        _coolDownToBreakItem = timeToBreakItemInSeconds;
    }


    // Update is called once per frame
    void Update()
    {
        if (_coolDownToBreakItem > 0)
        {
            _coolDownToBreakItem -= Time.deltaTime;
        } else
        {
            _coolDownToBreakItem = timeToBreakItemInSeconds;
            BreakItem();
        }
    }

    private void BreakItem()
    {
        if (_listOfItems.Count == 0) return;

        Item _item = _listOfItems[UnityEngine.Random.Range(0, _listOfItems.Count)];
        _item.Quebrar();
        _listOfItems.Remove(_item);
    }

    private void GetAllItems()
    {
        _listOfItems.AddRange(FindObjectsOfType<Item>());
        foreach(Item _item in _listOfItems)
        {
            _item.GetLocalItem().ReturnedItem += ReturnItemToList;
        }
    }

    public void ReturnItemToList(Item _item)
    {
        _listOfItems.Add(_item);
    }
}
