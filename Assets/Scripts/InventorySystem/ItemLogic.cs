using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isDragging = false;
    private float clickTime = 0f;
    private const float clickThreshold = 0.2f; // Tiempo máximo para considerar click

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
       
        clickTime = Time.time;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;

        InventoryManager.instance.SelectedItem = this;

    }

    void OnMouseDrag()
    {
        
        if (Time.time - clickTime > 0.1f)
        {
            isDragging = true;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (rb != null)
            {
                rb.MovePosition(mousePosition + offset);
            }
            else
            {
                transform.position = mousePosition + offset;
            }

           
        }
    }

    void OnMouseUp()
    {
        if (!isDragging)
        {
            Debug.Log($"Click en item: {itemData?.itemName}");
        }
        else
        {
            if (dropZone != null)
            {
                bool success = dropZone.ActivateAction(itemData);
                Debug.Log($"Drop en dropZone: {success}");
            }
            transform.localPosition = Vector3.zero;
        }
        isDragging = false;
    }

    public void OnData(ItemData d)
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
    private void OnTriggerExit2D(Collider2D other)
    {
        DropZone dz = other.GetComponent<DropZone>();
        if (dz != null && dz == dropZone)
        {
            dropZone = null;
        }
    }
}
