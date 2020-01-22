using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Models
{
    public class DataRepository : IDataRepository
    {
        private Dictionary<Guid, List<Dice>> _diceSets = new Dictionary<Guid, List<Dice>>();

        public Guid addDiceSet(List<Dice> dices)
        {
            Guid id = Guid.NewGuid();
            _diceSets.Add(id, dices);
            return id;
        }

        public Dictionary<Guid, List<Dice>> getAllDiceSets()
        {
            return _diceSets;
        }

        public Dictionary<Guid, List<Dice>> removeDiceSet(Guid id)
        {
            if(!_diceSets.Keys.Contains(id))
            {
                return _diceSets;
            }

            _diceSets = _diceSets.Where((kv) => kv.Key != id).ToDictionary(kv => kv.Key, kv => kv.Value);
            return _diceSets;
        }
    }
}
