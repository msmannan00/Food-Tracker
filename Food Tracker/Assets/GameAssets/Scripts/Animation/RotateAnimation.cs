using UnityEngine;
using DG.Tweening;

public class RotateAnimation : MonoBehaviour
{
    public RectTransform imageTransform;
    public float rotationSpeed = 0f; // Halved rotation speed

    private bool isActive = true;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!isActive)
        {
            return;
        }

        float rotationAngle = -360f;
        float rotationTime = Mathf.Abs(rotationAngle) / rotationSpeed;

        imageTransform.DORotate(new Vector3(0, 0, rotationAngle), rotationTime * 3, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(Rotate);
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void OnEnable()
    {
        isActive = true;
        Rotate();
    }
}
