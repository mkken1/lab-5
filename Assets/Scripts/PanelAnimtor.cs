using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlidingPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private Vector2 hiddenPosition; 
    [SerializeField] private Vector2 visiblePosition; 
    [SerializeField] private float duration = 0.3f;

    private bool isVisible = false;

    void Start()
    {
        panel.anchoredPosition = hiddenPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        if (panel == null)
        {
            Debug.LogError("Panel not assigned!");
            return;
        }

        if (isVisible)
            StartCoroutine(MoveTo(hiddenPosition));
        else
            StartCoroutine(MoveTo(visiblePosition));

        isVisible = !isVisible;
    }

    private IEnumerator MoveTo(Vector2 targetPos)
    {
        Vector2 startPos = panel.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = targetPos;
    }
}