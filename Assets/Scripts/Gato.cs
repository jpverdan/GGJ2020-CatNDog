using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum CatStates { WALKING_AROUND, GOING_TO_BREAK }

public class Gato : MonoBehaviour
{
    private List<Item> _listOfItems = new List<Item>();
    public float timeToBreakItemInSeconds = 3f;
    private float _coolDownToBreakItem;
    private Seeker _seeker;
    private CatStates _catState = CatStates.WALKING_AROUND;

    private Item _itemToBreak = null;

    IAstarAI _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<IAstarAI>();
        GetAllItems();
        _coolDownToBreakItem = timeToBreakItemInSeconds;
        _seeker = GetComponent<Seeker>();
    }


    // Update is called once per frame
    void Update()
    {
        if (_coolDownToBreakItem > 0)
        {
            _coolDownToBreakItem -= Time.deltaTime;
        }

        switch (_catState)
        {
            case CatStates.WALKING_AROUND:
                // choose point
                // walk to point
                if (_coolDownToBreakItem <= 0 && _listOfItems.Count > 0)
                {
                    _itemToBreak = ChooseItem();
                    _agent.destination = _itemToBreak.transform.position;
                    _agent.SearchPath();
                    StartCoroutine(GoingToBreakItem());
                    _catState = CatStates.GOING_TO_BREAK;
                }
                break;
            case CatStates.GOING_TO_BREAK:
                break;
            default:
                break;
        }
    }

    IEnumerator GoingToBreakItem()
    {
        float _cooldown = 2f;
        while (_agent.remainingDistance >= _agent.radius || _cooldown > 0)
        {
            _cooldown -= .5f;
            yield return new WaitForSeconds(.5f);
            print("Coroutine");

        }
        BreakItem(_itemToBreak);
        _coolDownToBreakItem = timeToBreakItemInSeconds;
        _catState = CatStates.WALKING_AROUND;
    }

    private void BreakItem(Item _item)
    {
        if (_listOfItems.Count == 0) return;
        
        _item.Quebrar();
        _itemToBreak = null;
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

    Item ChooseItem()
    {
        return _listOfItems[UnityEngine.Random.Range(0, _listOfItems.Count)];
    }
}
