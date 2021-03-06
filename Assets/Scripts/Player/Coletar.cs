using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Coletar : MonoBehaviour {

    private List<Parte> _inventario = new List<Parte>();
    private Parte _parteSelecionada = null;
    private LocalItem _localItemSelecionado = null;
    public float raioSpawn;
    public Collider2D spawnArea;
    private AudioSource audioSource;
    public AudioClip somColetar;
    public AudioClip somDropar;
    
    private void Start() {
        audioSource = GetComponent<AudioSource>();        
    }


    private void Update() {

        if(Input.GetButtonDown("Grab") && _parteSelecionada != null)
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

        if(Input.GetButtonDown("Put") && _localItemSelecionado != null)
        {
            if(_localItemSelecionado.LocalCerto(_inventario))
            {
                if(_localItemSelecionado.TemTodasAsPartes(_inventario))
                {
                    _localItemSelecionado.RetornaItem();
                    _inventario.Clear();
                }
            }
        }

        if(Input.GetButtonDown("Drop"))
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
        audioSource.PlayOneShot(somColetar);
        _parte.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
        _inventario.Add(_parte);
    }

    void DropPartes()
    {
        if(_inventario.Count==0) return;
    
        audioSource.PlayOneShot(somDropar);
        
        foreach (var _parte in _inventario)
        {
            float _x = UnityEngine.Random.Range(-raioSpawn, raioSpawn);
            float _y = UnityEngine.Random.Range(-raioSpawn, raioSpawn);
            _parte.transform.position = spawnArea.ClosestPoint(transform.position + new Vector3(_x, _y, 0));
            _parte.GetComponent<SpriteRenderer>().gameObject.SetActive(true);
        }
        _inventario.Clear();
    }

    void SelecionaParte(Parte _parte)
    {

        _parte.Highlight();
        _parteSelecionada = _parte;
    }

    private void RemoveSelecaoParte(Parte parte)
    {
        if(_parteSelecionada != null)
        {
            _parteSelecionada.RemoveHighlight();
            _parteSelecionada = null;
        }
    }

    private void SelecionaLocalItem(LocalItem localItem)
    {
        _localItemSelecionado = localItem;
        _localItemSelecionado.Highlight();
    }

    private void RemoveSelecaoLocalItem(LocalItem localItem)
    {
        if(_localItemSelecionado != null)
        {
            _localItemSelecionado.RemoveHighlight();
            _localItemSelecionado = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioSpawn);
    }
}