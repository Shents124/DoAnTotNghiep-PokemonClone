using UnityEngine;

namespace Moves
{
    [System.Serializable]
    public class LearnableMove
    {
        [SerializeField] private BaseMove baseMove;
        [SerializeField] private int levelLearn;

        public BaseMove BaseMove => baseMove;

        public int LevelLearn => levelLearn;
    }
}
