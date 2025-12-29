using System.Collections.Generic;
using App.Scripts.Features.Game.Constants;
using App.Scripts.Features.Game.Level.Components;
using App.Scripts.Features.Game.Level.Models;
using App.Scripts.Features.Game.Player.Components;
using App.Scripts.Infrastructure.Extensions;
using App.Scripts.Infrastructure.WorldExtesions.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace App.Scripts.Features.Game.Player.Systems
{
    public class InitializerPlayerInventory : InitializerBase
    {
        public override void OnAwake()
        {
            var modelLevelFilter = World.Filter.With<ModelLevel>().Build();
            var modelLevelEntity = modelLevelFilter.First();
            if (modelLevelEntity == null) return;

            var model = modelLevelEntity.GetComponent<ModelLevel>();

            Filter fieldCardFilter = World.Filter.With<Card>().With<OnField>().Build();
            
            List<Card> inventoryCards = new List<Card>();

            foreach (var entity in fieldCardFilter)
            {
                var fieldCard = entity.GetComponent<Card>();
                
                // Чтобы "закрыть" карту, нужно совпадение либо по типу (цвету), либо по номеру
                if (Random.value > 0.5f)
                {
                    // Совпадение по типу
                    inventoryCards.Add(new Card
                    {
                        type = fieldCard.type,
                        number = GameConstants.GetRandomValue()
                    });
                }
                else
                {
                    // Совпадение по номеру
                    inventoryCards.Add(new Card
                    {
                        type = CardType.GetRandom(),
                        number = fieldCard.number
                    });
                }
            }

            // Добавляем дополнительные карты
            for (int i = 0; i < model.countAdditional; i++)
            {
                inventoryCards.Add(new Card
                {
                    type = CardType.GetRandom(),
                    number = GameConstants.GetRandomValue()
                });
            }

            // Перемешиваем инвентарь
            inventoryCards.Shuffle();

            // Создаем сущность инвентаря
            var inventoryEntity = World.CreateEntity();
            inventoryEntity.SetComponent(new Inventory
            {
                cards = inventoryCards
            });
        }
    }
}