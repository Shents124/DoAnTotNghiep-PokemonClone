using Pokemons;

namespace Moves
{
    public class PokemonMove
    {
        private BaseMove baseMove;
        private int pp;

        public PokemonMove(BaseMove baseMove)
        {
            this.baseMove = baseMove;
            this.pp = baseMove.Pp;
        }
        public PokemonMove(BaseMove baseMove, int pp)
        {
            this.baseMove = baseMove;
            this.pp = pp;
        }

        public void UseMove()
        {
            pp--;
        }

        public int Id => baseMove.Id;
        public int Power() => baseMove.Power;
        public string GetName() => baseMove.MoveName;
        public PokemonType GetMoveType() => baseMove.Type;
        public int GetPP => pp;
        public bool CanUseMove() => pp > 0;

        public void RecoverMove()
        {
            pp = baseMove.Pp;
        }
    }
}
