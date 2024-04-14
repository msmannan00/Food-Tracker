using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FooterController : MonoBehaviour
{
    public Image homeImage;
    public Image planImage;
    public Image mealImage;
    public Image infoImage;

    private Color defaultColor = new Color32(0x8C, 0x8C, 0x8C, 0xFF);
    private Color selectedColor = new Color(0.027f, 0.494f, 0.224f);

    private Image lastSelectedImage = null;

    void Start()
    {
        SetImageColor(homeImage, defaultColor);
        SetImageColor(planImage, defaultColor);
        SetImageColor(mealImage, defaultColor);
        SetImageColor(infoImage, defaultColor);

        AddEventTrigger(homeImage, () => SelectImage(homeImage));
        AddEventTrigger(planImage, () => SelectImage(planImage));
        AddEventTrigger(mealImage, () => SelectImage(mealImage));
        AddEventTrigger(infoImage, () => SelectImage(infoImage));

        SelectImage(homeImage);
    }

    void AddEventTrigger(Image image, System.Action action)
    {
        EventTrigger trigger = image.gameObject.GetComponent<EventTrigger>() ?? image.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        entry.callback.AddListener((data) => { action(); });
        trigger.triggers.Add(entry);
    }

    void SelectImage(Image selectedImage)
    {
        if (lastSelectedImage != null)
        {
            SetImageColor(lastSelectedImage, defaultColor);
        }

        SetImageColor(selectedImage, selectedColor);
        lastSelectedImage = selectedImage;
    }

    void SetImageColor(Image image, Color color)
    {
        image.color = color;
    }
}
