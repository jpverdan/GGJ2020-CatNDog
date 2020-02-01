using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Coletar : MonoBehaviour {

    private List<Parte> _inventario = new List<Parte>();
    public float raioSpawn;

    private void Start() {
        
    }


    private void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("pegouuuu");
        if(other.CompareTag("Parte"))
        {
            if(_inventario.Count == 0)
            {
                ColetarParte(other.GetComponent<Parte>());
            }
            else
            {
                if(other.transform.parent == _inventario[0].transform.parent)
                {
                    ColetarParte(other.GetComponent<Parte>());
                }
                else
                {
                    DropPartes();
                    ColetarParte(other.GetComponent<Parte>());
                }
            }

        }
    }

    void ColetarParte(Parte _parte)
    {
        _parte.GetComponent<SpriteRenderer>().enabled = false;
        _inventario.Add(_parte);
    }

    void DropPartes()
    {
        foreach (var _parte in _inventario)
        {
            float _x = UnityEngine.Random.Range(-raioSpawn, raioSpawn);
            float _y = UnityEngine.Random.Range(-raioSpawn, raioSpawn);
            _parte.transform.position = transform.position + new Vector3(_x, _y, 0);
            _parte.GetComponent<SpriteRenderer>().enabled = true;
        }
        _inventario.Clear();
    }
}