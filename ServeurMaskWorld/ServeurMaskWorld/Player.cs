using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ServeurMaskWorld
{
    class Player
    {
        public int id;
        public string username;
        public string character;

        public Vector3 position;
        public Quaternion rotation;

        private float moveSpeed = 5f / ConstantsServer.TICKS_PER_SEC;
        private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new bool[4];
        }

        public void Update()
        {

        }


        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
    }
}
