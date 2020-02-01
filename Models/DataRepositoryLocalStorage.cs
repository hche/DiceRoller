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
        private Dictionary<string, List<Dice>> _diceSets = new Dictionary<string, List<Dice>>();
        private ILocalStorageService _localStorage;

        public DataRepositoryLocalStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> addDiceSet(List<Dice> dices)
        {
            var sets = await loadSets();
            string id = Guid.NewGuid().ToString();
            sets.Add(id, dices);
            await storeSets(sets);
            return id;
        }


        public async Task updateDiceSet(string id, List<Dice> dices)
        {
            var sets = await loadSets();
            var setsNew = sets.Where(s => s.Key != id).ToDictionary(kv => kv.Key, kv => kv.Value);
            setsNew.Add(id, dices);
            await storeSets(setsNew);
        }

        public Task<Dictionary<string, List<Dice>>> getAllDiceSets()
        {
            return loadSets();
        }

        public async Task<Dictionary<string, List<Dice>>> removeDiceSet(string id)
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

        
        private async Task<Dictionary<string, List<Dice>>> loadSets()
        {
            if (!_diceSets.Any())
            {
                var storedSets = await _localStorage.GetItemAsync<Dictionary<string, List<Dice>>>(STORAGE_KEY);
                if (storedSets != null)
                {
                    _diceSets = storedSets;
                }
            }

            return _diceSets;
        }
        private Task storeSets(Dictionary<string, List<Dice>> sets)
        {
            _diceSets = sets;
            return _localStorage.SetItemAsync(STORAGE_KEY, sets);
        }
    }
}
