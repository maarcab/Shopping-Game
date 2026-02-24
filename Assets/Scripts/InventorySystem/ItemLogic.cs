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
        // Registrar el tiempo del click
        clickTime = Time.time;

        // Calcular offset para el drag
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;

        // Seleccionar el item inmediatamente
        InventoryManager.instance.SelectedItem = this;

        // Opcional: feedback visual de selección
        // transform.localScale = Vector3.one * 1.1f;
    }

    void OnMouseDrag()
    {
        // Solo hacer drag si ha pasado un pequeño tiempo (para diferenciar de click)
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

            // Opcional: cambiar layer mientras se arrastra para evitar interacciones
            // gameObject.layer = LayerMask.NameToLayer("Dragging");
        }
    }

    void OnMouseUp()
    {
        // Si fue un click (no hubo drag significativo)
        if (!isDragging)
        {
            // No hacer nada, mantener la selección para el botón
            Debug.Log($"Click en item: {itemData?.itemName}");
        }
        else
        {
            // Fue un drag, comprobar drop zone
            if (dropZone != null)
            {
                bool success = dropZone.ActivateAction(itemData);
                Debug.Log($"Drop en dropZone: {success}");
            }

            // Resetear posición al slot
            transform.localPosition = Vector3.zero;
        }

        // Resetear estados
        isDragging = false;

        // Opcional: restaurar escala y layer
        // transform.localScale = Vector3.one;
        // gameObject.layer = LayerMask.NameToLayer("Default");
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

    // Método para limpiar la dropZone cuando se sale de ella
    private void OnTriggerExit2D(Collider2D other)
    {
        DropZone dz = other.GetComponent<DropZone>();
        if (dz != null && dz == dropZone)
        {
            dropZone = null;
        }
    }
}
