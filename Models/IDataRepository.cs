using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Models
{
    interface IDataRepository
    {
        public Task<Dictionary<string, List<Dice>>> getAllDiceSets();

        public Task<Dictionary<string, List<Dice>>> removeDiceSet(string id);

        public Task<string> addDiceSet(List<Dice> dices);

        public Task updateDiceSet(string id, List<Dice> dices);
    }
}
