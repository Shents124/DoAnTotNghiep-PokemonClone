using System.Collections.Generic;
using Pokemons;

namespace SaveSystem
{
    [System.Serializable]
    public class SerializedPokemonData
    {
        public int id;
        public int level;
        public int currentHp;
        public List<SerializedMoveData> moveData;
        
        public SerializedPokemonData (Pokemon pokemon)
        {
            var basePokemon = pokemon.BasePokemon;
            id = basePokemon.Id;
            level = pokemon.Level;
            currentHp = pokemon.CurrentHp;
            
            moveData = new List<SerializedMoveData>();
            foreach (var move in pokemon.PokemonMoves)
            {
                moveData.Add(new SerializedMoveData(move));
            }
        }
    }
}