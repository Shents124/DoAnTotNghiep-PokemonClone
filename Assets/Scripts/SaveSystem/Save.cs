using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class Save
    {
        public List<SerializedPokemonData> pokemonPartyStack = new List<SerializedPokemonData>();
        public List<SerializedPokemonData> pokemonPcStack = new List<SerializedPokemonData>();
        public float xLocation;
        public float yLocation;

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void LoadFormJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}