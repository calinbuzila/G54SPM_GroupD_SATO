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
        private static float boundaryX;
        private static float boundaryZ;
        public int NrOfEnemies
        {
            get
            {
                return nrOfEnemies;
            }
            set
            {
                nrOfEnemies = value;
            }
        }
        private static float positionScaleX;
        private static float positionScaleZ;
        private string name;
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
        public float BoundaryX
        {
            get
            {
                return boundaryX;
            }
            set
            {
                boundaryX = value;
            }
        }
        public float BoundaryZ
        {
            get
            {
                return boundaryZ;
            }
            set
            {
                boundaryZ = value;
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
