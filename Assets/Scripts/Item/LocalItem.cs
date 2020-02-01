using UnityEngine;
using System.Collections.Generic;

public class LocalItem : MonoBehaviour {

    public Item item;
    public Color corOriginal;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = corOriginal;
    }

    public bool LocalCerto(List<Parte> listaDeColetados)
    {
        if(listaDeColetados.Count>0)
        {
        return listaDeColetados[0].transform.parent == item.transform;
        }
        else
        {
            return false;
        }
    }

    public bool TemTodasAsPartes(List<Parte> listaDeColetados)
    {
        return item.listaDePartes.Length == listaDeColetados.Count;
    }

    public void RetornaItem()
    {
        item.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void DestroiPartes()
    {
        foreach (var _parte in item.listaDePartes)
        {
            Destroy(_parte.gameObject);
        }
    }
}