using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy
    {
        #region properties and variables

        public static int NrOfEnemies = 0;
        public static float BoundaryX;
        public static float BoundaryZ;

        public static float PositionScaleX;
        public static float PositionScaleZ;
        private string name;
        public static Dictionary<float, float> positionings;

       
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
      

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Enemy()
        {
            positionings = new Dictionary<float, float>();
        }

        /// <summary>
        /// increment the number of enemies, the static field is holding the number of enemies that were spawned
        /// </summary>
        public void IncreaseEnemies()
        {
            NrOfEnemies += 1;
        }

        public void DecreaseEnemies()
        {
            NrOfEnemies -= 1;
        }

    }
}
