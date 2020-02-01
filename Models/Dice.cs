using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Models
{
    public class Dice
    {
        private int[] rolls = new int[] { -1 };
        private Random rnd = new Random();
        
        public Guid Id { get; set; }

        public int Sides { get; set; }

        public int Value { get { return rolls[rolls.Length - 1]; } }

        public int PreviousValue
        {
            get
            {
                return rolls.Length > 1 ? rolls[rolls.Length - 2] : rolls[rolls.Length - 1];
            }
        }

        public string Name { get { return $"D{Sides}"; } }

        public Dice()
        {
            Id = Guid.NewGuid();
        }

        public int Roll()
        {
            var roll = rnd.Next(1, Sides + 1);
            int[] updatedRolls = new int[rolls.Length + 1];
            rolls.CopyTo(updatedRolls, 0);
            updatedRolls[updatedRolls.Length - 1] = roll;

            this.rolls = updatedRolls;
            return this.Value;
        }
    }
}
