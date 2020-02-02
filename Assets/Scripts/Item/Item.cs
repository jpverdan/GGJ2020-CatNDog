using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Parte[] listaDePartes;
    public float raioSpawn;
    public Collider2D spawnArea;
    [HideInInspector] public LocalItem localItem;
    public AudioClip itemSomQuebrar;
    private AudioSource audioSource;

    [SerializeField] private int _scoreValue = 0;

    Color _origColor;
    Color _ghostColor;
    SpriteRenderer _spriteRendererComponent;

    bool _placed = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _spriteRendererComponent = GetComponent<SpriteRenderer>();
        _origColor = _spriteRendererComponent.color;
        _ghostColor = _origColor;
        _ghostColor.a = .2f;
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
        audioSource.PlayOneShot(itemSomQuebrar);
        _placed = false;
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (var _parte in listaDePartes)
        {
            var _objeto = Instantiate(_parte, GetRandomPointInsideSpawnArea(), Quaternion.identity);
            _objeto.transform.parent = transform;
        }
    }

    public void Place()
    {
        _placed = true;
        _spriteRendererComponent.color = _origColor;
    }

    public void ShowGhostItem()
    {
        if (_placed) return;
        _spriteRendererComponent.enabled = true;
        _spriteRendererComponent.color = _ghostColor;

    }

    public void HideGhostItem()
    {
        if (_placed) return;
        _spriteRendererComponent.enabled = false;
        _spriteRendererComponent.color = _origColor;
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
