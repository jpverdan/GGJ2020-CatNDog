using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Coletar : MonoBehaviour {

    private List<Parte> _inventario = new List<Parte>();
    private Parte _parteSelecionada = null;
    private LocalItem _localItemSelecionado = null;
    public float raioSpawn;


    private void Start() {
        
    }


    private void Update() {

        if(Input.GetKeyDown(KeyCode.Z) && _parteSelecionada != null)
        {
            if (_inventario.Count == 0)
            {
                ColetarParte(_parteSelecionada.GetComponent<Parte>());
            }
            else
            {
                if (_parteSelecionada.transform.parent == _inventario[0].transform.parent)
                {
                    ColetarParte(_parteSelecionada.GetComponent<Parte>());
                }
                else
                {
                    DropPartes();
                    ColetarParte(_parteSelecionada.GetComponent<Parte>());
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.C) && _localItemSelecionado != null)
        {
            print("vc clicou C");
            if(_localItemSelecionado.LocalCerto(_inventario))
            {
                print("retornou ao lugar certo");
                if(_localItemSelecionado.TemTodasAsPartes(_inventario))
                {
                    print("tem todas as partes");
                    _localItemSelecionado.RetornaItem();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            DropPartes();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Parte") && _parteSelecionada == null)
        {
            SelecionaParte(other.GetComponent<Parte>());
        }

        if(other.CompareTag("LocalItem"))
        {
            SelecionaLocalItem(other.GetComponent<LocalItem>());
        }
    }

    private void SelecionaLocalItem(LocalItem localItem)
    {
        _localItemSelecionado = localItem;
        _localItemSelecionado.GetComponent<SpriteRenderer>().color = Color.black;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Parte"))
        {
            RemoveSelecaoParte(other.GetComponent<Parte>());
        }

        if(other.CompareTag("LocalItem"))
        {
            RemoveSelecaoLocalItem(other.GetComponent<LocalItem>());
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

    void SelecionaParte(Parte _parte)
    {
        _parte.GetComponent<SpriteRenderer>().color = Color.black;
        _parteSelecionada = _parte;
    }

    private void RemoveSelecaoParte(Parte parte)
    {
        if(_parteSelecionada != null)
        {
        _parteSelecionada.GetComponent<SpriteRenderer>().color = _parteSelecionada.GetComponent<Parte>().corOriginal;
        _parteSelecionada = null;
        }
    }

    private void RemoveSelecaoLocalItem(LocalItem localItem)
    {
        if(_localItemSelecionado != null)
        {
            _localItemSelecionado.GetComponent<SpriteRenderer>().color = _localItemSelecionado.GetComponent<LocalItem>().corOriginal;
            _localItemSelecionado = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioSpawn);
    }
}