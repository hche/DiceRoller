using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Models
{
    interface IDataRepository
    {
        public Task<Dictionary<Guid, List<Dice>>> getAllDiceSets();

        public Task<Dictionary<Guid, List<Dice>>> removeDiceSet(Guid id);

        public Task<Guid> addDiceSet(List<Dice> dices);

        public Task updateDiceSet(Guid id, List<Dice> dices);
    }
}
