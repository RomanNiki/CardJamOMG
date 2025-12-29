using App.Scripts.Infrastructure.BaseView;
using App.Scripts.Infrastructure.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Game.Views
{
    public class ViewCard : MonoView
    {
        public Image[] cardBackgrounds;
        public TMP_Text[] texts;
        public Graphic[] Graphic;

        public Rect GetRect()
        {
            return cardBackgrounds[0].rectTransform.GetWorldRect();
        }

        public void SetNumber(int number)
        {
            foreach (TMP_Text tmpText in texts)
            {
                tmpText.SetText(number.ToString());
            }
        }

        public void SetSprite(Sprite sprite)
        {
            foreach (var cardBackground in cardBackgrounds)
            {
                cardBackground.sprite = sprite;
            }
        }
        
        public void SetColor(Color color)
        {
            foreach (var cardBackground in Graphic)
            {
                cardBackground.color = color;
            }
        }
    }
}