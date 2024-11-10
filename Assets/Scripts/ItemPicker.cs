using pickable.testObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPicker : MonoBehaviour
{
    public GameObject TextObject;
    private float pickDistance = 100f;
    private Camera _mainCamera;
    private TextMeshProUGUI pickUpText;

    private void Awake()
    {
        _mainCamera = gameObject.GetComponent<Camera>();
        pickUpText = TextObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        CheckForPickableItem();
    }

    private void CheckForPickableItem()
    {
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickDistance))
        {
            Pickable pickable = hit.collider.GetComponent<Pickable>();
            if (pickable != null)
            {
                pickUpText.SetText("Press F to pick up");

                if (Input.GetKeyDown(KeyCode.F))
                {
                    pickable.OnPickUp();
                    pickUpText.SetText("");
                }
            }
            else
            {
                pickUpText.SetText("");
            }
        }
        else
        {
            pickUpText.SetText("");
        }
    }
}
