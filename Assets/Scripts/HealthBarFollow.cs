using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform characterTransform; // Assign your character's transform here
    public Vector3 offset; // Adjust this offset to position the HP bar correctly

    private RectTransform hpBarRect;
    private Camera gameCamera;

    void Start()
    {
        hpBarRect = GetComponent<RectTransform>(); // Get the RectTransform of the HP bar
        gameCamera = Camera.main; // Assume the main camera is the game camera
    }

    void Update()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(gameCamera, characterTransform.position + offset);
        hpBarRect.position = screenPoint;
    }
}
