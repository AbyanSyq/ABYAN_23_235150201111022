using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

    
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float currenthealth;
    public Image healthImage;
    public Gradient colorGradient;
    private void Start() {
        MaxHealth(currenthealth);
    }
    public void MaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
        healthImage.color = colorGradient.Evaluate(1f);
    }
    public void SetHealth(float magnitude){
        Debug.Log(magnitude);
        currenthealth += magnitude;
        slider.value = currenthealth;
        if(currenthealth <= 0){

        }
        healthImage.color = colorGradient.Evaluate(slider.normalizedValue);
    }
}
