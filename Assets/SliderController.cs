using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider slider;
    public TMP_Text textField;
    public int Value { get { return (int)slider.value; } set { slider.value = value; } }
    public string Text { get { return textField.text; } }
    
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        textField.text = ((int)slider.value).ToString();
    }
}
