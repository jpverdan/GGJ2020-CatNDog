using UnityEngine;
using System.Collections.Generic;

public class LocalItem : MonoBehaviour {

    public Item item;
    public Color corOriginal;

    public delegate void ItemEvent(Item _item);
    public ItemEvent ReturnedItem;

    public GameObject highlightBasePrefab;
    private SpriteRenderer highlightSpriteComponent;

    private void Awake()
    {
        print(this.gameObject.name);
        item.localItem = this;
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = corOriginal;
        var obj = Instantiate(highlightBasePrefab, transform);
        highlightSpriteComponent = obj.GetComponent<SpriteRenderer>();
        highlightSpriteComponent.sprite = GetComponent<SpriteRenderer>().sprite;
        highlightSpriteComponent.enabled = false;

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
        ReturnedItem(item);
    }

    private void DestroiPartes()
    {
        foreach (var _parte in item.listaDePartes)
        {
            Destroy(_parte.gameObject);
        }
    }

    public void Highlight()
    {
        highlightSpriteComponent.enabled = true;
    }

    public void RemoveHighlight()
    {
        highlightSpriteComponent.enabled = false;
    }
}