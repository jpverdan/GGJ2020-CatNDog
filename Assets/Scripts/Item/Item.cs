using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Parte[] listaDePartes;
    public float raioSpawn;
    public Collider2D spawnArea;
    [HideInInspector] public LocalItem localItem;

    [SerializeField] private int _scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetScoreValue()
    {
        return _scoreValue;
    }

    public void Quebrar()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (var _parte in listaDePartes)
        {
            var _objeto = Instantiate(_parte, GetRandomPointInsideSpawnArea(), Quaternion.identity);
            _objeto.transform.parent = transform;
        }
    }

    Vector2 GetRandomPointInsideSpawnArea()
    {
        float _x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float _y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        return spawnArea.ClosestPoint(new Vector2(_x, _y));
    }

    public LocalItem GetLocalItem()
    {
        return localItem;
    }

}
