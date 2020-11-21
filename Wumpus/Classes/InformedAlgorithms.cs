using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Wumpus.Classes
{
    class InformedAlgorithms
    {
        //private Form1 _formInstance;
        private KnowledgeUnit[,] _map;
        private Dictionary<Point, Point> _cameFrom;
        private Dictionary<Point, int> _costSoFar;
        private List<KeyValuePair<int, Point>> _frontier;
        private Point _endPoint;
        private Point _startPoint;
        private Point _targetPoint;
        private bool _explore;

        public Point TargetPoint { get => _targetPoint; set => _targetPoint = value; }

        public InformedAlgorithms(int startX, int startY, KnowledgeUnit[,] map, bool explore = true)
        {
            _explore = explore;
            _map = map;
            //_foodPoint = FoodCoordinate();
            _startPoint = new Point(startX, startY);
            _cameFrom = new Dictionary<Point, Point>();
            _frontier = new List<KeyValuePair<int, Point>>();
            _costSoFar = new Dictionary<Point, int>();

            _frontier.Add(new KeyValuePair<int, Point>(0, new Point(startX, startY)));
            _costSoFar.Add(_startPoint, 0);
            _cameFrom.Add(_startPoint, new Point(-1, -1));

            //_formInstance = form;
        }

        //public List<Point> AStarAlgorithm()
        //{
        //    while (_frontier.Count != 0)
        //    {
        //        var current = _frontier.Last();
        //        _frontier.RemoveAt(_frontier.Count - 1);

        //        if (_map[current.Value.Y, current.Value.X].IsSave())
        //        {
        //            break;
        //        }
        //        foreach (var next in GetNeighbours(current.Value))
        //        {
        //            int cost = _costSoFar[current.Value] + Utilities.Cost(current.Value, next);
        //            if (!_costSoFar.ContainsKey(next) || cost < _costSoFar[next])
        //            {
        //                _costSoFar[next] = cost;

        //                int priority = cost + Utilities.Heuristic(_foodPoint, next);
        //                _frontier.Add(new KeyValuePair<int, Point>(priority, next));
                        
        //                _cameFrom[next] = current.Value;
        //                _frontier.Sort(delegate (KeyValuePair<int, Point> p1, KeyValuePair<int, Point> p2) { return p1.Key > p2.Key ? -1 : 1; });
        //            }
        //        }
        //    }
        //    List<Point> result = TrackFromEnd();
        //    PrintResult(result);
        //    return result;
        //}

        public List<Point> GreedyAlgorithm()
        {
            while(_frontier.Count != 0)
            {
                var current = _frontier.Last();
                _frontier.RemoveAt(_frontier.Count - 1);

                if (_explore)
                {
                    if (_map[current.Value.Y, current.Value.X].IsSave() && !_map[current.Value.Y, current.Value.X].Visited)
                    {
                        _endPoint = new Point(current.Value.X, current.Value.Y);
                        break;
                    }
                }
                else
                {
                    if (current.Value.Y == TargetPoint.Y && current.Value.X == TargetPoint.X)
                    {
                        _endPoint = new Point(current.Value.X, current.Value.Y);
                        break;
                    }
                }
                


                foreach(var next in GetNeighbours(current.Value))
                {
                    if (!_cameFrom.ContainsKey(next))
                    {
                        //int priority = Utilities.Heuristic(_foodPoint, next);
                        _frontier.Add(new KeyValuePair<int, Point>(0, next));
                        _cameFrom.Add(next, current.Value);
                        _frontier.Sort(delegate (KeyValuePair<int, Point> p1, KeyValuePair<int, Point> p2) { return p1.Key > p2.Key ? -1 : 1; });
                    }
                }
            }
            List<Point> result = TrackFromEnd();
            PrintResult(result);
            return result;
        }


        private List<Point> GetNeighbours(Point p)
        {
            List<Point> result = new List<Point>();
            if (p.X > 0 && _map[p.Y, p.X - 1].IsSave()) result.Add(new Point(p.X - 1, p.Y));
            if (p.X < _map.GetLength(1)-1 && _map[p.Y, p.X + 1].IsSave()) result.Add(new Point(p.X + 1, p.Y));
            if (p.Y > 0 && _map[p.Y-1, p.X].IsSave()) result.Add(new Point(p.X, p.Y-1));
            if (p.Y < _map.GetLength(0)-1 && _map[p.Y + 1, p.X].IsSave()) result.Add(new Point(p.X, p.Y + 1));
            return result;
        }

        private List<Point> TrackFromEnd()
        {
            List<Point> result = new List<Point>();
            result.Add(_endPoint);
            while(result[result.Count - 1] != _startPoint)
            {
                if(_cameFrom.ContainsKey(result[result.Count - 1]))
                {
                    result.Add(_cameFrom[result[result.Count - 1]]);
                }
                else
                {
                    //Console.WriteLine("The food doesn't exist");
                    break;
                }
            }
            result.Reverse();
            return result;
        }

        private void PrintResult(List<Point> result)
        {
            foreach(var p in result)
            {
                Console.WriteLine(String.Concat(p.X, " , ", p.Y));
            }
            
        }
    }
}
