using System;

namespace RC5
{
    public class Random
    {
        private uint _previousValue;
        private uint _m;
        private uint _a;
        private uint _c;

        public Random()
        {
            _previousValue = 0;
            _m = (2 << 9) - 1;
            _a = 2 << 5;
            _c = 0;
        }

        public Random(uint m, uint a, uint c, uint seed)
        {
            _m = (uint)((2 << (int)m - 1) - 1);
            if (_m > 0 && a < _m && c < _m && seed < _m)
            {
                _previousValue = seed;
                _a = a;
                _c = c;
            }
            else  //2147483648 
                throw new Exception("Bad input values");
        }

        public uint NextValue
        {
            get
            {
                var value = (_a * _previousValue + _c) % _m;
                _previousValue = value;
                return value;
            }
        }
    }
}
