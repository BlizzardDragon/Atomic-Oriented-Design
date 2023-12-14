using System;

namespace AtomicOrientedDesign.Shooter
{
    public class MoneyStorage
    {
        public int Money => _money;

        private int _money = 1000000;


        public void SpendMoney(int value)
        {
            if ((_money - value) < 0)
            {
                throw new ArgumentOutOfRangeException("Not enough money!");
            }

            _money -= value;
        }
    }
}