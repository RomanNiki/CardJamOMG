using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Game.Views
{
    public class ViewCard : MonoBehaviour
    {
        public Image image;
        public TMP_Text text;

        public void SetNumber(int number)
        {
            text.SetText(number.ToString());
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}