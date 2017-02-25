using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Enemy
    {
        #region properties and variables

        private static int nrOfEnemies = 0;
        public int NrOfEnemies
        {
            get
            {
                return nrOfEnemies;
            }
        }

        private float positionScaleX;
        private float positionScaleZ;
        public static List<Dictionary<float, float>> positionings;

        public float PositionScaleX
        {
            get
            {
                return positionScaleX;
            }
            set
            {
                positionScaleX = value;
            }
        }
        public float PositionScaleZ
        {
            get
            {
                return positionScaleZ;
            }
            set
            {
                positionScaleZ = value;
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Enemy()
        {

        }

        /// <summary>
        /// increment the number of enemies, the static field is holding the number of enemies that were spawned
        /// </summary>
        public void increaseEnemies()
        {
            nrOfEnemies += 1;
        }


    }
}
