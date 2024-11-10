using pickable.testObject;
using UnityEngine;

public class TestPickUpActionTwo : MonoBehaviour, Pickable
{
    private Renderer rend;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void OnPickUp()
    {
        Debug.Log("Press F to pick up");
        if (Input.GetKeyDown(KeyCode.F)) {
            rend.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}