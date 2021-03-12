using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.AI
{
    class Vertex
    {
        public List<Vertex> _nextVertices;
        public List<bool> _visitedVertices;
        public List<short> _directions;

        public int _cameFrom;

        public Vertex(List<short> directions)
        {
            _nextVertices = new List<Vertex>();
            _visitedVertices = new List<bool>();
            _directions = new List<short>();
            foreach(short dir in directions)
            {
                _nextVertices.Add(new Vertex());
                _visitedVertices.Add(false);
                _directions.Add(dir);
            }
        }

        public Vertex()
        {
            _nextVertices = new List<Vertex>();
            _visitedVertices = new List<bool>();
            _directions = new List<short>();
        }

        public void FillNewKnowledge(List<short> knowledge)
        {
            foreach(short k in knowledge)
            {
                _nextVertices.Add(new Vertex());
                _visitedVertices.Add(false);
                _directions.Add(k);
            }
        }

        public bool NoNewPaths()
        {
            return _nextVertices.Count == 0;
        }

        public void ResetVisited()
        {
            for(int i = 0; i< _visitedVertices.Count; i++)
            {
                _visitedVertices[i] = false;
            }      
        }

        public bool CheckIfSomeNotVisited()
        {
            return _visitedVertices.Contains(false);
        }

        public KeyValuePair<Vertex,int> NotVisited()
        {
            for(int i = 0; i < _visitedVertices.Count; i++)
            {
                if (!_visitedVertices[i])
                {
                    _visitedVertices[i] = true;
                    return new KeyValuePair<Vertex, int>(_nextVertices[i], _directions[i]);
                }
            }
            return new KeyValuePair<Vertex, int>();
        }

        public int DirToVertex(Vertex v)
        {
            for(int i = 0; i < _visitedVertices.Count; i++)
            {
                if(_nextVertices[i] == v)
                {
                    return _directions[i];
                }
            }
            return 0;
        }

        public int GetMemoryAmount()
        {
            int counter = 0;
            for(int i = 0; i< _visitedVertices.Count; i++)
            {
                counter += 3;   //1 byte for bool(visited/not visited) two bites for short(direction where can go)
            }
            return counter;
        }
    }
}
