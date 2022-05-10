using System.Collections.Generic;
using Moves;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pokemons
{
    [System.Serializable]
    public class Pokemon
    {
        [SerializeField] private BasePokemon basePokemon;
        [SerializeField] private int level;
        private List<PokemonMove> pokemonMoves;
        private int currentHp;
        private readonly int maxHp;
        private readonly int attack;
        private readonly int defense;
        private readonly int speed;
        
        public Pokemon(BasePokemon basePokemon, int level)
        {
            this.basePokemon = basePokemon;
            this.level = level;
            maxHp = Mathf.FloorToInt((basePokemon.MaxHp * 2 * level) / 100f + level + 10);
            currentHp = maxHp;
            attack = Mathf.FloorToInt((basePokemon.Attack * 2 * level) / 100f + 5);
            defense = Mathf.FloorToInt((basePokemon.Defense * 2 * level) / 100f + 5);
            speed = Mathf.FloorToInt((basePokemon.Speed * 2 * level) / 100f + 5);
        }
        
        public int MaxHp => maxHp;
        public int Attack => attack;
        public int Defense => defense;
        public int Speed => speed;
        public int CurrentHp => currentHp;
        public int Level => level;
        public BasePokemon BasePokemon => basePokemon;
        public List<PokemonMove> PokemonMoves => pokemonMoves;

        public void SetCurrentHp(int value)
        {
            currentHp = value;
        }
        
        public void SetMove(List<PokemonMove> pokemonMoves)
        {
            this.pokemonMoves = pokemonMoves;
        }
        
        public void InitBaseMove()
        {
            pokemonMoves = new List<PokemonMove>();
            foreach (var move in basePokemon.LearnableMoves)
            {
                if(move.LevelLearn < level)
                    pokemonMoves.Add(new PokemonMove(move.BaseMove));
                if(pokemonMoves.Count >= 4)
                    break;
            }
        }

        public void GetDamage(float attack, float powerMove, int levelEnemy, float dameEffectType)
        {
            var dameTaken = ((2 * levelEnemy / 5 + 2) * powerMove * attack / Defense / 50 + 2) * dameEffectType;
            currentHp -= Mathf.RoundToInt(dameTaken);
            if (currentHp < 0)
                currentHp = 0;
        }

        public bool IsDead()
        {
            return currentHp <= 0;
        }

        public PokemonMove GetRandomMove()
        {
            var index = Random.Range(0, pokemonMoves.Count);
            return pokemonMoves[index];
        }

        public void Recover()
        {
            currentHp = maxHp;
            foreach (var move in pokemonMoves)
            {
                move.RecoverMove();
            }
        }
    }
}
