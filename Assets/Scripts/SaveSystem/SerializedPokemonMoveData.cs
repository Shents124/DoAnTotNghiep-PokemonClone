using Moves;
using Pokemons;

namespace SaveSystem
{
    [System.Serializable]
    public class SerializedPokemonMoveData
    {
        private string name;
        private string description;
        private PokemonType type;
        private int power;
        private int accuracy;
        private int pp;
        private int levelLearn;

        public void ParseData(LearnableMove learnableMove)
        {
            var baseMove = learnableMove.BaseMove;

            name = baseMove.MoveName;
            description = baseMove.Description;
            type = baseMove.Type;
            power = baseMove.Power;
            accuracy = baseMove.Accuracy;
            pp = baseMove.Accuracy;
            levelLearn = learnableMove.LevelLearn;
        }
    }
}