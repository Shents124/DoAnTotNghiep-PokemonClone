using System;
using Pokemons;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUI
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] private Image imgType;
        [SerializeField] private Text lbName;
        [SerializeField] private Text lbPP;
        private int currentPP;
        private Action<int> onclick;
        private int index;
        public void SetData(int index, PokemonType type, string name, int pp, Action<int> onClick)
        {
            imgType.sprite = GetSpriteType(type);
            lbName.text = name;
            currentPP = pp;
            lbPP.text = currentPP.ToString();
            this.onclick = onClick;
            this.index = index;
        }

        private Sprite GetSpriteType(PokemonType type)
        {
            Sprite sprite = null;
            sprite = Resources.Load<Sprite>($"PokemonType/type{type}");
            return sprite;
        }

        public void UpdatePp(int pp)
        {
            lbPP.text = pp.ToString();
        }
        public void OnClickBtn()
        {
            onclick?.Invoke(index);
        }
        
    }
}
