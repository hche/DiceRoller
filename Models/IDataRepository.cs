using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Models
{
    interface IDataRepository
    {
        public Dictionary<Guid, List<Dice>> getAllDiceSets();
        public Dictionary<Guid, List<Dice>> removeDiceSet(Guid id);

        public Guid addDiceSet(List<Dice> dices);
    }
}
