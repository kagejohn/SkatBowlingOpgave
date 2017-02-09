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
        public string success { get; set; }
    }
}