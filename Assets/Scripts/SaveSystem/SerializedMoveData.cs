using Moves;

namespace SaveSystem
{
    [System.Serializable]
    public class SerializedMoveData
    {
        public int id;
        public int pp;

        public SerializedMoveData(PokemonMove move)
        {
            id = move.Id;
            pp = move.GetPP;
        }
    }
}