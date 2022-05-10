using System.Collections.Generic;
using Moves;
using Pokemons;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "BaseMoveStorage", menuName = "Save System/BaseMoveStorage")]
    public class BaseMoveStorage: ScriptableObject
    {
        [SerializeField] private List<BaseMove> listMove;

        private readonly Dictionary<int, BaseMove> moveDic = new Dictionary<int, BaseMove>();
        
        public void LoadBaseMoveData()
        {
            foreach (var move in listMove)
            {
                moveDic.Add(move.Id, move);
            }
        }

        public BaseMove GetBaseMoveById(int id)
        {
            moveDic.TryGetValue(id, out var baseMove);
            return baseMove;
        }
    }
}