using System.Collections.Generic;
using Moves;
using UnityEngine;

namespace Pokemons
{
    [CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create a new Pokemon")]
    public class BasePokemon : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string pokemonName;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite frontSprite;
        [SerializeField] private Sprite backSprite;

        [SerializeField] private PokemonType type1;
        [SerializeField] private PokemonType type2;

        [Header("Base Stats")] 
        [SerializeField] private int maxHp;
        [SerializeField] private int attack;
        [SerializeField] private int defense;
        [SerializeField] private int speed;
        [SerializeField] private int rate;

        [SerializeField] private List<LearnableMove> learnableMoves;

        public int Id => id;

        public string PokemonName => pokemonName;

        public Sprite FrontSprite => frontSprite;

        public Sprite BackSprite => backSprite;

        public Sprite Icon => icon;
        
        public PokemonType Type1 => type1;

        public PokemonType Type2 => type2;

        public int MaxHp => maxHp;

        public int Attack => attack;

        public int Defense => defense;

        public int Speed => speed;
        public int Rate => rate;

        public List<LearnableMove> LearnableMoves => learnableMoves;
    }

    public enum PokemonType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass
    }
}