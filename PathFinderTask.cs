using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var minValue = new double[] { int.MaxValue };
            var result = new List<int[]>();
            MakePermutations(new int[checkpoints.Length],
                             1,
                             result,
                             checkpoints,
                             minValue);
            minValue[0] = int.MaxValue;
            return result[result.Count - 1];
        }

        static void MakePermutations(int[] permutation,
                                     int position,
                                     List<int[]> result,
                                     Point[] checkpoints,
                                     double[] minValue)
        {
            if (position == permutation.Length)
            {
                minValue[0] = PointExtensions.GetPathLength(checkpoints, permutation);
                result.Add(permutation.ToArray());
            }
            else
            {
                for (int i = 1; i < permutation.Length; i++)
                {
                    var index = Array.IndexOf(permutation, i, 0, position);

                    if (index != -1)
                    {
                        continue;
                    }

                    permutation[position] = i;

                    if (PointExtensions.GetPathLength(checkpoints,
                                                      permutation.Take(position + 1).ToArray()) >= minValue[0])
                    {
                        continue;
                    }

                    MakePermutations(permutation, position + 1, result, checkpoints, minValue);
                }
            }
        }
    }
}