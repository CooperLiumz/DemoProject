using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class MyTween
    {
        public double SineIn (float _time , float _beginValue , float _changeValue , float _duration)
        {
            return -_changeValue * Math.Cos (_time / _duration * ( Math.PI / 2 )) + _changeValue + _beginValue;
                
        }

        public double SineOut (float _time , float _beginValue , float _changeValue , float _duration)
        {
            return _changeValue * Math.Cos (_time / _duration * ( Math.PI / 2 )) + _beginValue;               

        }

        public double CircIn (float _time , float _beginValue , float _changeValue , float _duration)
        {
            return -_changeValue * Math.Sqrt (1 - (_time / _duration) * _time - 1 ) + _beginValue;

        }

        public double CircOut (float _time , float _beginValue , float _changeValue , float _duration)
        {
            return _changeValue * Math.Sqrt (1 - ( _time / _duration-1 ) * _time - 1) + _beginValue;

        }

    }
}
