﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibOCR.Neural.Response
{
    public class DataShift
    {
        public int Index { get; set; } = 0;
        public char Value
        {
            get
            {
                switch (Index)
                {
                    case 0:
                        return '0';
                    case 1:
                        return '1';
                    case 2:
                        return '2';
                    case 3:
                        return '3';
                    case 4:
                        return '4';
                    case 5:
                        return '5';
                    case 6:
                        return '6';
                    case 7:
                        return '7';
                    case 8:
                        return '8';
                    case 9:
                        return '9';
                    case 10:
                        return 'A';
                    case 11:
                        return 'B';
                    case 12:
                        return 'C';
                    case 13:
                        return 'D';
                    case 14:
                        return 'E';
                    default:
                        return ' ';
                }
            }
        }
        public double Confidence { get; set; } = 0;

        public DataShift(int index, double conf)
        {
            Index = index;
            Confidence = conf;
        }
    }
}
