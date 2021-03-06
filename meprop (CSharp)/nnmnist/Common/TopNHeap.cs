﻿using System;
using System.Linq;

namespace nnmnist.Common
{
    class TopNHeap
    {
        private readonly double[] _vHeap;
        private readonly int[] _iHeap;
        private int _nTop;

        public TopNHeap(int nTop)
        {
            _nTop = nTop;
            _vHeap = new double[nTop];
            _iHeap = new int[nTop];
        }

        public int[] GetTop(double[] val, int top)
        {
            for (var i = 0; i < top; i++)
            {
                _vHeap[i] = val[i];
                _iHeap[i] = i;
            }
            MakeHeap(top);
            for (var i = top; i < val.Length; i++)
            {
                if (val[i] > _vHeap[0])
                {
                    _vHeap[0] = val[i];
                    _iHeap[0] = i;
                    ShiftDown(0, top);
                }
            }
            return _iHeap;
        }

        public int[] GetAbsTop(double[] val, int top)
        {
            if (top > _nTop)
            {
                throw new ArgumentException("top too big");
            }
            if (top > val.Length)
            {
                throw new ArgumentException("value too short");
            }
            for (var i = 0; i < top; i++)
            {
                _vHeap[i] = Math.Abs(val[i]);
                _iHeap[i] = i;
            }
            MakeHeap(top);
            for (var i = top; i < val.Length; i++)
            {
                var abs = Math.Abs(val[i]);
                if (abs > _vHeap[0])
                {
                    _vHeap[0] = abs;
                    _iHeap[0] = i;
                    ShiftDown(0, top);
                }
            }
            return _iHeap;
        }

        private void MakeHeap(int top)
        {
            for (var i = top >> 1; i >= 0; i--)
            {
                ShiftDown(i, top);
            }
        }

        private void ShiftDown(int cur, int top)
        {
            while (true)
            {
                int lc = (cur << 1) + 1, rc = lc + 1;
                if (lc >= top)
                {
                    return;
                }
                int nxt;
                if (rc >= top)
                {
                    nxt = lc;
                }
                else
                {
                    nxt = _vHeap[lc] > _vHeap[rc] ? rc : lc;
                }
                if (_vHeap[cur] > _vHeap[nxt])
                {
                    var ftmp = _vHeap[cur];
                    _vHeap[cur] = _vHeap[nxt];
                    _vHeap[nxt] = ftmp;
                    var itmp = _iHeap[cur];
                    _iHeap[cur] = _iHeap[nxt];
                    _iHeap[nxt] = itmp;
                    cur = nxt;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
