using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.UI
{
   [ExecuteAlways]
public class RadialLayout : MonoBehaviour
{
    public int slotCount = 8;
    public float radiusPercent = 0.4f;

    private RectTransform _rectTransform;
    private List<RectTransform> _children = new();
    private Image _image;

#if UNITY_EDITOR
    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        CacheChildren();
        ApplyLayout();

        Undo.undoRedoPerformed += ApplyLayout;
    }

    private void OnDisable()
    {
        Undo.undoRedoPerformed -= ApplyLayout;
    }
#else
    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        CacheChildren();
        ApplyLayout();
    }
#endif

    private void OnRectTransformDimensionsChange()
    {
        ApplyLayout();
    }

    private void CacheChildren()
    {
        _children.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) is RectTransform child)
                _children.Add(child);
        }
    }

    private void ApplyLayout()
    {
        if (_rectTransform == null || _children.Count == 0 || slotCount <= 0)
            return;

        Vector2 usableSize = GetVisibleSize(_rectTransform);
        float radius = Mathf.Min(usableSize.x, usableSize.y) * radiusPercent;

        for (int i = 0; i < _children.Count; i++)
        {
            float angle = (360f / slotCount) * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
            RectTransform child = _children[i];
            child.anchoredPosition = pos;
            child.localRotation = Quaternion.Euler(0, 0, angle - 90f);

            float scaleFactor = usableSize.x / 500f;
            child.localScale = Vector3.one * scaleFactor;
        }
    }

    private Vector2 GetVisibleSize(RectTransform rectTransform)
    {
        Vector2 rawSize = rectTransform.rect.size;

        if (_image != null && _image.preserveAspect && _image.sprite != null)
        {
            float imageAspect = _image.sprite.bounds.size.x / _image.sprite.bounds.size.y;
            float rectAspect = rawSize.x / rawSize.y;

            if (imageAspect > rectAspect)
                return new Vector2(rawSize.x, rawSize.x / imageAspect);
            else
                return new Vector2(rawSize.y * imageAspect, rawSize.y);
        }

        return rawSize;
    }
}


}