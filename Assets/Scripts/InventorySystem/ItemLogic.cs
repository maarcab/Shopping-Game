using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody2D rb;
    private Collider2D col;

    public ItemData itemData;
    private SpriteRenderer spriteRenderer;
    private DropZone dropZone;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.linearVelocity = Vector2.zero;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        rb.MovePosition(mousePosition + offset);
    }

    void OnMouseUp()
    {
        if (dropZone != null)
        {
            bool success = dropZone.ActivateAction(itemData);

            if (success)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localPosition = Vector3.zero;
                ButtonLogic.SelectItem(gameObject, itemData);
            }
        }
        else
        {
            transform.localPosition = Vector3.zero;
            ButtonLogic.SelectItem(gameObject, itemData);
        }
    }
    void OnData(ItemData d)
    {
        itemData = d;
        spriteRenderer = GetComponent<SpriteRenderer>();


        if (itemData != null && spriteRenderer != null)
        {

            spriteRenderer.sprite = itemData.icon;


        }
    }
    public void CheckZoneCollision(DropZone dz)
    {
        dropZone = dz;
    }

}
