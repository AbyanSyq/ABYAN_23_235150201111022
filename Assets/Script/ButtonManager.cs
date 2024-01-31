using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Sprite defaulSprite;
    public Sprite onPress;
    public Image image;
    
    public void OnClick(){
        image.sprite = onPress;
    }
}
