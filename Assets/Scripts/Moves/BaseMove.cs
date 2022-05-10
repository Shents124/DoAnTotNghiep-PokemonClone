using Pokemons;
using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "Pokemon Move", menuName = "Pokemon/Create a new Pokemon Move")]
    public class BaseMove : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string moveName;
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private PokemonType type;
        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        [SerializeField] private int pp;

        public int Id => id;
        public string MoveName => moveName;

        public string Description => description;

        public PokemonType Type => type;

        public int Power => power;

        public int Accuracy => accuracy;

        public int Pp => pp;
    }
}
