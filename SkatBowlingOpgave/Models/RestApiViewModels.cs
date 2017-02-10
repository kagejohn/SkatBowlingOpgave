using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SkatBowlingOpgave.Consume;
using SkatBowlingOpgave.DataModels;

namespace SkatBowlingOpgave.Models
{
    public class RestApiViewModels
    {
        private RestApiConsume restApiConsume = new RestApiConsume();
        /// <summary>
        /// Denne list indeholder en liste af de summerede resultater
        /// </summary>
        private List<int> pointResultater = new List<int>();
        /// <summary>
        /// Denne list indeholder en boolean der indikere om runden var en strike
        /// </summary>
        private List<bool> strike = new List<bool>();
        /// <summary>
        /// Denne list indeholder en boolean der indikere om runden var en spare
        /// </summary>
        private List<bool> spare = new List<bool>();
        /// <summary>
        /// Denne string indeholder den nuværende token
        /// </summary>
        private string token = "";

        /// <summary>
        /// Denne metode behandler arrayet af resultater den modtager fra REST API'en
        /// </summary>
        /// <returns>Denne metode returnere en liste af de resultater den modtager fra REST API'en, de summerede resultater og om de blev uploadet korrekt til REST API'en</returns>
        public async Task<PointResultater> Bowlingpoints()
        {
            var points = await restApiConsume.GetPointsAsync();
            for (int i = 0; i < points.points.Length; i++)
            {
                if (points.points[i][0] == 10)
                {
                    if (i < 10)
                    {
                        if (points.points[i][1] == 0)
                        {
                            pointResultater.Add(points.points[i][0]);
                        }
                        else
                        {
                            pointResultater.Add(points.points[i][0] + points.points[i][1]);
                        }
                    }
                    if (points.points[i][0] == 10 && points.points[i][1] == 0 && i < points.points.Length - 1)
                    {
                        Strike(i, points.points[i][0], points.points[i + 1][0]);
                    }
                    else
                    {
                        Strike(i, points.points[i][0], points.points[i][1]);
                    }
                    Spare(i, points.points[i][0]);
                    strike.Add(true);
                    spare.Add(false);
                }
                else if (points.points[i].Sum() == 10 && points.points[i][0] != 10)
                {
                    if (i < 10)
                    {
                        pointResultater.Add(points.points[i].Sum());
                    }
                    Strike(i, points.points[i][0], points.points[i][1]);
                    Spare(i, points.points[i][0]);
                    strike.Add(false);
                    spare.Add(true);
                }
                else
                {
                    if (i < 10)
                    {
                        pointResultater.Add(points.points[i].Sum());
                    }
                    Strike(i, points.points[i][0], points.points[i][1]);
                    Spare(i, points.points[i][0]);
                    strike.Add(false);
                    spare.Add(false);
                }
                if (i >= 1 && i < 10)
                {
                    pointResultater[i] += pointResultater[i - 1];
                }
            }
            PointResultater resultater = new PointResultater();
            resultater.points = points.points;
            resultater.pointResultater = pointResultater.ToArray();
            token = points.token;
            resultater.success = await restApiConsume.PostPointsAsync(resultater, token);
            return resultater;
        }

        /// <summary>
        /// Denne metode tilføjer points fra de næste 2 kugler til resultatet fra den sidste runde hvis sidste runde var en strike
        /// </summary>
        /// <param name="i">Det nuværende index</param>
        /// <param name="firstPoint">Den næste kugle</param>
        /// <param name="secondPoint">Den anden næste kugle</param>
        private void Strike(int i, int firstPoint, int secondPoint)
        {
            if (i >= 1)
            {
                if (strike[i - 1])
                {
                    pointResultater[i - 1] += firstPoint;
                    pointResultater[i - 1] += secondPoint;
                }
            }
        }

        /// <summary>
        /// Denne metode tilføjer points fra den næste kugle til resultatet fra den sidste runde hvis sidste runde var en spare
        /// </summary>
        /// <param name="i">Det nuværende index</param>
        /// <param name="firstPoint">Den næste kugle</param>
        private void Spare(int i, int firstPoint)
        {
            if (i >= 1)
            {
                if (spare[i - 1])
                {
                    pointResultater[i - 1] += firstPoint;
                }
            }
        }
    }
}