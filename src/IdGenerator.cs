using System;
using System.Collections.Concurrent;
using System.Threading;

namespace IdGenerator
{
    public class IdGenerator
    {
        ulong _maximun;
        ulong _minimun;
        ulong _current;
        ulong _maxlength;
        ConcurrentQueue<ulong> _ids;
        bool _useup;

        public IdGenerator()
        {
            _maximun = ulong.MaxValue;
            _minimun = ulong.MinValue;
            _maxlength = _maximun - _minimun;
            _current = _minimun;
            _ids = new();
        }

        public IdGenerator(ulong maximun, ulong minimun = ulong.MinValue)
        {
            _maximun = maximun;
            _minimun = minimun;
            _maxlength = _maximun - _minimun;
            _current = _minimun;
            _ids = new();
        }

        public IdGenerator(long maximun, long minimun = (long)ulong.MinValue)
        {
            if (maximun < 0) throw new ArgumentException("argument maximun is required more than zero.", nameof(maximun));
            if (minimun < 0) throw new ArgumentException("argument minimun is required more than zero.", nameof(maximun));
            if (maximun < minimun) throw new ArgumentException("argument maximun is required more than minimun.", nameof(maximun));
            _maximun = (ulong)maximun;
            _minimun = (ulong)minimun;
            _maxlength = _maximun - _minimun;
            _current = _minimun;
            _ids = new();
        }

        public IdGenerator(int maximun, int minimun = (int)ulong.MinValue)
        {
            if (maximun < 0) throw new ArgumentException("argument maximun is required more than zero.", nameof(maximun));
            if (minimun < 0) throw new ArgumentException("argument minimun is required more than zero.", nameof(maximun));
            if (maximun < minimun) throw new ArgumentException("argument maximun is required more than minimun.", nameof(maximun));
            _maximun = (ulong)maximun;
            _minimun = (ulong)minimun;
            _maxlength = _maximun - _minimun;
            _current = _minimun;
            _ids = new();
        }

        public IdGenerator(uint maximun, uint minimun = (uint)ulong.MinValue)
        {
            if (maximun < 0) throw new ArgumentException("argument maximun is required more than zero.", nameof(maximun));
            if (minimun < 0) throw new ArgumentException("argument minimun is required more than zero.", nameof(maximun));
            if (maximun < minimun) throw new ArgumentException("argument maximun is required more than minimun.", nameof(maximun));
            _maximun = (ulong)maximun;
            _minimun = (ulong)minimun;
            _maxlength = _maximun - _minimun;
            _current = _minimun;
            _ids = new();
        }

        public ulong RentId()
        {
            if (_useup)
            {
                if (_ids.Count == 0) return 0;
                return _ids.TryDequeue(out _current) ? _current : 0;
            }
            if (Interlocked.CompareExchange(ref _current, _minimun, _maximun) == _maximun)
            {
                _useup = true;
            }
            return Interlocked.Increment(ref _current);
        }

        public void ReturnId(ulong id)
        {
            _ids.Enqueue(id);
        }
    }
}
