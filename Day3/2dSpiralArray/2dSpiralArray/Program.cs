using System;
using System.Collections.Generic;

namespace _2dSpiralArray
{
    class Program
    {
        public static int[,] myArray;
        public static int size;
        public static int index;
        public static int target = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an target integer: ");
            string target_string = Console.ReadLine();
            target = int.Parse(target_string);

            int largest_element = FindGreatestValueOfSquare(target);
            Build2dArray(largest_element, target);
            
            //Console.WriteLine(PathToCenter(targetIndex));

            CloseOut();
        }

        static string PathToCenter(int[] target)
        {
            int width = target[0];
            int height = target[1];
            string path = "";
            int distanceToCenter = 0;

            if (width < 0 || height < 0 || height > index || width > index)
            {
                throw new Exception();
            }
            while (myArray[width, height] != 1)
            {
                char dir = GetDirectionOfCenter(width, height);
                path += dir + ", ";
                distanceToCenter++;

                int[] pos = MoveOneStepToCenter(dir, width, height);
                width = pos[0];
                height = pos[1];
            }

            return path + distanceToCenter;
        }

        static int[] MoveOneStepToCenter(char dir, int w, int h)
        {
            char _dir = dir;
            int _w = w;
            int _h = h;

            switch (_dir)
            {
                case 'l':
                    return new[] { _w - 1, _h };
                case 'u':
                    return new[] { _w, _h - 1 };
                case 'r':
                    return new[] { _w + 1, _h };
                case 'd':
                    return new[] { _w, _h + 1 };
                default:
                    return new[] { index, index };
            }

        }

        static char GetDirectionOfCenter(int width, int height)
        {
            int l_width = width - 1;
            int l_val = int.MaxValue;
            int r_width = width + 1;
            int r_val = int.MaxValue;
            int u_height = height - 1;
            int u_val = int.MaxValue;
            int d_height = height + 1;
            int d_val = int.MaxValue;

            if (l_width <= index && l_width >= 0) l_val = myArray[l_width, height];
            if (r_width <= index && r_width >= 0) r_val = myArray[r_width, height];
            if (u_height <= index && u_height >= 0) u_val = myArray[width, u_height];
            if (d_height <= index && d_height >= 0) d_val = myArray[width, d_height];

            int[] values = { l_val, r_val, u_val, d_val };
            int min_val = int.MaxValue;
            foreach (int val in values)
            {
                if (val < min_val) min_val = val;
            }

            char dir = '\x0';
            if (min_val == l_val)
            {
                dir = 'l';
            }
            else if (min_val == r_val)
            {
                dir = 'r';
            }
            else if (min_val == d_val)
            {
                dir = 'd';
            }
            else if (min_val == u_val)
            {
                dir = 'u';
            }

            return dir;
        }

        static int FindGreatestValueOfSquare(int target)
        {
            //find the next largest square
            size = (int)Math.Ceiling(Math.Sqrt(target));
            if (size % 2 == 0) size++;
            int largest_element = size * size;
            index = size - 1;
            Console.WriteLine(String.Format("size: {0} largest_element: {1} target: {2}", size, largest_element, target));

            return largest_element;
        }

        static void CloseOut()
        {
            Console.WriteLine("Closing...");
            Console.ReadKey();
        }

        /*
        static int[] Build2dArray(int largest_element, int target)
        {
            myArray = new int[size, size];
            int _largest = largest_element;
            char dir = 'l';
            int width = size - 1;
            int height = size - 1;
            int[] targetIndex = { index, index };

            Console.WriteLine(dir + " " + _largest + " w:" + width + " h:" + height);
            myArray[width, height] = _largest--;
            while ( _largest > 0)
            {
                while( HasNextPosInDir(width, height, dir) && _largest > 0 )
                {
                    int[] pos = NextPos(width, height, dir);
                    width = pos[0];
                    height = pos[1];

                    if( _largest == target )
                    {
                        targetIndex = new[]{ width, height };
                    }

                    myArray[width, height] = _largest--;
                }
                dir = NextDir(dir);
            }

            return targetIndex;
        }*/

        public static int[] targetIndex = { 0, 0 };
        public static char mydir = 'r';
        static void Build2dArray(int largest_element, int target)
        {
            myArray = new int[size, size];
            int _largest = largest_element;
            int value = 1;
            int length = 1;
            int width = size / 2;
            int height = size / 2;

            Console.WriteLine(mydir + " " + _largest + " w:" + width + " h:" + height);
            myArray[width, height] = value++;
            while (HasNextPosInDir(width, height, mydir))
            {
                Console.WriteLine(mydir + " " + _largest + " w:" + width + " h:" + height);
                //int[] res = InitNextSides(width, height, length, value);
                int[] res = InitNextSidesDiffValue(width, height, length);
                width = res[0];
                height = res[1];
                value = res[2];
                if (value > target) break;
                length++;
            }
        }

        public static int[] InitNextSides(int _w, int _h, int _l, int _v)
        {
            for (int sides = 0; sides < 2; sides++)
            {
                for (int i = 0; i < _l; i++)
                {
                    if (HasNextPosInDir(_w, _h, mydir))
                    {
                        int[] pos = NextPos(_w, _h, mydir);
                        _w = pos[0];
                        _h = pos[1];
                        myArray[_w, _h] = _v;
                        if (_v == target)
                        {
                            targetIndex = new[] { _w, _h };
                        }
                        _v++;
                    }
                }
                mydir = NextDir(mydir);
            }            

            return new[] { _w, _h, _v };
        }

        public static int[] InitNextSidesDiffValue(int _w, int _h, int _l)
        {
            int _v = 0;
            for (int sides = 0; sides < 2; sides++)
            {
                for (int i = 0; i < _l; i++)
                {
                    if (HasNextPosInDir(_w, _h, mydir))
                    {
                        int[] pos = NextPos(_w, _h, mydir);
                        _w = pos[0];
                        _h = pos[1];
                        _v = CalculateValueAtPosition(_w, _h);
                        if (_v > target)
                        {
                            Console.WriteLine(_v);
                            return new[] { _w, _h, _v };
                        }
                        myArray[_w, _h] = _v;
                    }
                }
                mydir = NextDir(mydir);
            }

            return new[] { _w, _h, _v};
        }

        public static int CalculateValueAtPosition(int width, int height)
        {
            int sum = 0;

            if (width + 1 <= size && height - 1 > 0) sum += myArray[width + 1, height - 1];
            if (width + 1 <= size) sum += myArray[width + 1, height];
            if (width + 1 <= size && height + 1 <= size) sum += myArray[width + 1, height + 1];
            if (height + 1 <= size) sum += myArray[width, height + 1];
            if (width - 1 > 0 && height + 1 <= size) sum += myArray[width - 1, height + 1];
            if (width - 1 > 0) sum += myArray[width - 1, height];
            if (width - 1 > 0 && height - 1 > 0) sum += myArray[width - 1, height - 1];
            if (height - 1 > 0) sum += myArray[width, height - 1];

            return sum;
        }

        static bool CanAssignCurrentPos(int _w, int _h)
        {
            if( myArray[_w, _h] == 0)
            {
                return true;
            }

            return false;
        }

        static char NextDir(char dir)
        {
            char _dir = dir;

            switch (_dir)
            {
                case 'l':
                    return 'd';
                case 'u':
                    return 'l';
                case 'r':
                    return 'u';
                case 'd':
                    return 'r';
                default:
                    return '\0';
            }
        }

        static int[] NextPos(int width, int height, char dir)
        {
            int _w = width;
            int _h = height;

            switch (dir)
            {
                case 'l':
                    _w--;
                    break;
                case 'u':
                    _h--;
                    break;
                case 'r':
                    _w++;
                    break;
                case 'd':
                    _h++;
                    break;
                default:
                    break;
            }

            return new[]{ _w, _h};
        }

        static bool HasNextPosInDir(int width, int height, char dir)
        {
            int[] ret = NextPos(width, height, dir);
            int _w = ret[0];
            int _h = ret[1];

            if( _w < 0 || _h < 0 || _h > index || _w > index)
            {
                return false;
            }
            else if( myArray[_w,_h] == 0 )
            {
                return true;
            }

            return false;
        }
    }
}
