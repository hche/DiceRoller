using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace DiceRoller.Models
{
    public class DataRepositoryLocalStorage : IDataRepository
    {

        private const string STORAGE_KEY = "diceSetStore";
        private Dictionary<Guid, List<Dice>> _diceSets = new Dictionary<Guid, List<Dice>>();
        private ILocalStorageService _localStorage;

        public DataRepositoryLocalStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<Guid> addDiceSet(List<Dice> dices)
        {
            var sets = await loadSets();
            Guid id = Guid.NewGuid();
            sets.Add(id, dices);
            await storeSets(sets);
            return id;
        }


        public async Task updateDiceSet(Guid id, List<Dice> dices)
        {
            var sets = await loadSets();
            var setsNew = sets.Where(s => s.Key != id).ToDictionary(kv => kv.Key, kv => kv.Value);
            setsNew.Add(id, dices);
            await storeSets(setsNew);
        }

        public Task<Dictionary<Guid, List<Dice>>> getAllDiceSets()
        {
            return loadSets();
        }

        public async Task<Dictionary<Guid, List<Dice>>> removeDiceSet(Guid id)
        {
            var sets = await loadSets();
            if(!sets.Keys.Contains(id))
            {
                return sets;
            }

            var setNew = sets.Where((kv) => kv.Key != id).ToDictionary(kv => kv.Key, kv => kv.Value);
            await storeSets(setNew);
            return setNew;
        }

        
        private async Task<Dictionary<Guid, List<Dice>>> loadSets()
        {
            if (!_diceSets.Any())
            {
                var storedSets = await _localStorage.GetItemAsync<Dictionary<Guid, List<Dice>>>(STORAGE_KEY);
                if (storedSets != null)
                {
                    _diceSets = storedSets;
                }
            }

            return _diceSets;
        }
        private Task storeSets(Dictionary<Guid, List<Dice>> sets)
        {
            _diceSets = sets;
            return _localStorage.SetItemAsync(STORAGE_KEY, sets);
        }
    }
}
