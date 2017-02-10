using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkatBowlingOpgave.DataModels
{
    public class PointResultater
    {
        public int[][] points { get; set; }
        public int[] pointResultater { get; set; }
        /// <summary>
        /// Denne property bliver sat baseret på om POST kaldet kommer tilbage med successe eller ej
        /// </summary>
        public string success { get; set; }
    }
}