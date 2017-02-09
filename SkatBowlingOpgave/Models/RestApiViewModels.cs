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
        private List<int> pointResultater = new List<int>();
        private List<bool> strike = new List<bool>();
        private List<bool> spare = new List<bool>();
        private string token = "";

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