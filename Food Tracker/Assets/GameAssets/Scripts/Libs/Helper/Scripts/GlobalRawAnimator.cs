using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GlobalRawAnimator : MonoBehaviour
{
    public void addButtonOutline(Image buttonImage)
    {
        Outline outline = buttonImage.gameObject.AddComponent<Outline>();
        outline.effectColor = new Color(9 / 255f, 126 / 255f, 57 / 255f, 0f);
        outline.effectDistance = new Vector2(0, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(outline.DOColor(new Color(9 / 255f, 126 / 255f, 57 / 255f, 1f), 0.25f));
        sequence.Join(DOVirtual.Float(0, 0f, 0.25f, value => outline.effectDistance = new Vector2(0, value)));
        sequence.Append(DOVirtual.Float(0f, 0, 0.25f, value => outline.effectDistance = new Vector2(0, value)));
    }

    public void removeButtonOutline(Image buttonImage)
    {
        Outline[] outlines = buttonImage.GetComponents<Outline>();
        if (outlines.Length > 0)
        {
            Outline outlineToRemove = outlines[outlines.Length - 1];

            Sequence sequence = DOTween.Sequence();
            sequence.Append(outlineToRemove.DOColor(new Color(outlineToRemove.effectColor.r, outlineToRemove.effectColor.g, outlineToRemove.effectColor.b, 0f), 0.25f));
            sequence.Join(DOVirtual.Float(outlineToRemove.effectDistance.y, 0, 0.25f, value => outlineToRemove.effectDistance = new Vector2(0, value)));
            sequence.OnComplete(() => Destroy(outlineToRemove));
        }
    }

    public void WobbleObject(GameObject gameObject)
    {
        float wobbleDuration = 0.45f;
        Vector3 wobbleStrength = new Vector3(1.05f, 1.05f, 1f);

        gameObject.transform.DOComplete();
        gameObject.transform.DOPunchScale(Vector3.one - wobbleStrength, wobbleDuration, 1, 0);
    }
}
