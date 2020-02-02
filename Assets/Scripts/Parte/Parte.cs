using UnityEngine;

public class Parte : MonoBehaviour {

    
    public GameObject highlightBasePrefab;
    private SpriteRenderer highlightSpriteComponent;
    public float highlightScale = 1.1f;

    private void Start()
    {
        var obj = Instantiate(highlightBasePrefab, transform);
        obj.transform.localScale = new Vector3(highlightScale, highlightScale);
        highlightSpriteComponent = obj.GetComponent<SpriteRenderer>();
        highlightSpriteComponent.sprite = GetComponent<SpriteRenderer>().sprite;
        highlightSpriteComponent.enabled = false;
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