using System;

namespace Day13
{
    public class Layer
    {
        public int? ScannerRange { get; }
        public int? ScannerPosition { get; private set; }
        private int _scannerDirection = 1;
        public int Depth { get; }
        public bool HasPacket { get; private set; }

        public Layer(int depth, int? scannerRange)
        {
            Depth = depth;
            ScannerRange = scannerRange;
            if (ScannerRange.HasValue)
            {
                ScannerPosition = 0;
            }
        }

        public void MoveScanner()
        {
            if (ScannerRange.HasValue)
            {
                ScannerPosition += _scannerDirection;

                if (ScannerPosition == 0 || ScannerPosition == (ScannerRange - 1))
                {
                    _scannerDirection *= -1;
                }
            }
        }

        public bool MoveIntoLayer()
        {
            //HasPacket = true;
            if (ScannerPosition.HasValue && ScannerPosition.Value == 0)
            {
                return true;
            }

            return false;
        }

        public void MoveOutOfLayer()
        {
            HasPacket = false;
        }

        public void ResetScanner()
        {
            if (ScannerPosition.HasValue)
            {
                ScannerPosition = 0;
                _scannerDirection = 1;
            }
        }

        public void PrintRow()
        {
            Console.Write($"{Depth}:");
            if (ScannerRange.HasValue)
            {
                for (int i = 0; i < ScannerRange.Value; i++)
                {
                    if (i == 0)
                    {
                        if (HasPacket && ScannerPosition == i)
                        {
                            Console.Write("[(S)]");
                        }
                        else if (HasPacket)
                        {
                            Console.Write("[( )]");
                        }
                        else if (ScannerPosition == i)
                        {
                            Console.Write("[S]");
                        }
                        else
                        {
                            Console.Write("[]");
                        }
                    }
                    else if (ScannerPosition == i)
                    {
                        Console.Write("[S]");
                    }
                    else
                    {
                        Console.Write("[]");
                    }
                }
            }
            Console.WriteLine();
        }

        public Layer Clone()
        {
            Layer ret = new Layer(Depth, ScannerRange.HasValue ? ScannerRange.Value: (int?)null);
            if (ScannerPosition.HasValue)
            {
                ret.ScannerPosition = ScannerPosition.Value;
            }

            ret._scannerDirection = _scannerDirection;

            return ret;
        }
    }
}
