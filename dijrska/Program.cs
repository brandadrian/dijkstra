using System;
using System.Collections.Generic;
using System.Linq;

namespace dijrska
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<List<int?>> matrix = new List<List<int?>>();
            List<KeyValuePair<int?, bool>> result = new List<KeyValuePair<int?, bool>>();

            int startingPoint = 0;
            matrix = BuildMatrix();
            result = BuildResultVector(startingPoint, matrix);
            result = BlockVectorRow(startingPoint, result);

            for (int i = 0; i < result.Count - 1; i++)
            {
                result = BuildVectorOfMinimum(matrix, result);
                Console.WriteLine(GetVectorAsString(result));
            }

            Console.WriteLine(GetVectorAsString(result));
            Console.ReadLine();
        }

        private static List<KeyValuePair<int?, bool>> BuildVectorOfMinimum(List<List<int?>> matrix, List<KeyValuePair<int?, bool>> vector)
        {
            List<KeyValuePair<int?, bool>> newVector = new List<KeyValuePair<int?, bool>>();
            int minimum = vector.Where(x => !x.Value && x.Key != null).ToList().Min(x => (int)x.Key);
            int position = -1;

            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i].Key.HasValue && (vector[i].Key.Value == minimum) && !vector[i].Value)
                {
                    position = i;
                }
            }

            vector = BlockVectorRow(position, vector);
            List<int?> matchingVector = matrix[position];

            for (int i = 0; i < vector.Count; i++)
            {
                if (matchingVector[i].HasValue && !vector[i].Value)
                {
                    if ((matchingVector[i].Value + minimum) > vector[i].Key)
                    {
                        newVector.Add(new KeyValuePair<int?, bool>(vector[i].Key, false));
                    }
                    else
                    {
                        newVector.Add(new KeyValuePair<int?, bool>(matchingVector[i].Value + minimum, false));
                    }
                }
                else
                {
                    newVector.Add(new KeyValuePair<int?, bool>(vector[i].Key, vector[i].Value));
                }
            }

            return newVector;
        }

        private static List<KeyValuePair<int?, bool>> BlockVectorRow(int position, List<KeyValuePair<int?, bool>> result)
        {
            result[position] = new KeyValuePair<int?, bool>(result[position].Key, true);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static List<List<int?>> BuildMatrix()
        {
            List<List<int?>> matrix = new List<List<int?>>();
            /*******************
            Matrix:
            A--2--B--10--E
            |    /|     /
            |   / |    /
            4  2  4   4
            | /   |  /
            |/    | /
            C--1--D

            Lösungschritte:
            Tabelle Knoten A | Kosten zum Knoten
            ---------------- | A   B   C   D   E
            Schritt  1 (init)| 0   2   4   n   n
            Schritt  2       | 0   2   4   n   n
            Schritt  3       | 0   2   4   6  12
            

            ********************/
            matrix.Add(new List<int?>() { 0, 2, 4, null, null });
            matrix.Add(new List<int?>() { 2, 0, 2, 4, 10 });
            matrix.Add(new List<int?>() { 4, 2, 0, 1, null });
            matrix.Add(new List<int?>() { null, 4, 1, 0, 4 });
            matrix.Add(new List<int?>() { null, 10, null, 4, 0 });

            //matrix.Add(new List<int?>() { 0, 1, 4, null, null });
            //matrix.Add(new List<int?>() { 1, 0, 1, 4, 10 });
            //matrix.Add(new List<int?>() { 4, 1, 0, 1, null });
            //matrix.Add(new List<int?>() { null, 4, 1, 0, 4 });
            //matrix.Add(new List<int?>() { null, 10, null, 4, 0 });

            //matrix.Add(new List<int?>() { 0, null, 4, null, null});
            //matrix.Add(new List<int?>() { null, 0, 2, 4, 10 });
            //matrix.Add(new List<int?>() { 4, 2, 0, 1, null });
            //matrix.Add(new List<int?>() { null, 4, 1, 0, 4 });
            //matrix.Add(new List<int?>() { null, 10, null, null, 0 });

            //matrix.Add(new List<int?>() { 0, 2, null, null, 7, 2, null, null });
            //matrix.Add(new List<int?>() { 2, 0, 2, null, null, null, null, null });
            //matrix.Add(new List<int?>() { null, 2, 0, 3, 1, null, null, null });
            //matrix.Add(new List<int?>() { null, null, 3, 0, null, null, null, 1 });
            //matrix.Add(new List<int?>() { 7, null, 1, null, 0, null, null, null });
            //matrix.Add(new List<int?>() { 2, null, null, null, null, 0, 1, null });
            //matrix.Add(new List<int?>() { null, null, 1, null, null, 1, 0, 6 });
            //matrix.Add(new List<int?>() { null, null, null, 1, null, null, 6, 0 });

            return matrix;
        }

        private static List<KeyValuePair<int?, bool>> BuildResultVector(int position, List<List<int?>> matrix)
        {
            List<KeyValuePair<int?, bool>> result = new List<KeyValuePair<int?, bool>>();
            foreach (int? element in matrix[position])
                result.Add(new KeyValuePair<int?, bool>(element, false));

            return result;
        }

        private static string GetVectorAsString(List<KeyValuePair<int?, bool>> vector)
        {
            string resultVector = string.Empty;
            string resultBoolVector = string.Empty;

            foreach (KeyValuePair<int?, bool> keyValuePair in vector)
                resultVector += keyValuePair.Key == null ? "NULL " : keyValuePair.Key.ToString() + " ";

            foreach (KeyValuePair<int?, bool> keyValuePair in vector)
                resultBoolVector += keyValuePair.Value.ToString() + " ";

            return resultVector + "| " + resultBoolVector;
        }
    }
}
