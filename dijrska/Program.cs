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
            List<KeyValuePair<int?, bool>> resultVector = new List<KeyValuePair<int?, bool>>();

            int startingPoint = 4;
            matrix = BuildMatrix();
            resultVector = BuildResultVector(startingPoint, matrix);
            resultVector = BlockVectorPosition(startingPoint, resultVector);

            for (int i = 0; i < resultVector.Count - 1; i++)
            {
                resultVector = BuildVectorOfMinimum(matrix, resultVector);
                Console.WriteLine(GetVectorAsString(resultVector));
            }

            Console.WriteLine(GetVectorAsString(resultVector));
            Console.ReadLine();
        }

        private static List<KeyValuePair<int?, bool>> BuildVectorOfMinimum(List<List<int?>> matrix, List<KeyValuePair<int?, bool>> vector)
        {
            int vectorMinimumValue = vector.Where(x => !x.Value && x.Key != null).ToList().Min(x => (int)x.Key);
            int positionOfMinimumValue = -1;

            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i].Key.HasValue && (vector[i].Key.Value == vectorMinimumValue) && !vector[i].Value)
                {
                    positionOfMinimumValue = i;
                }
            }

            vector = BlockVectorPosition(positionOfMinimumValue, vector);

            List<int?> vectorWithMinimumValue = matrix[positionOfMinimumValue];
            List<KeyValuePair<int?, bool>> newVector = new List<KeyValuePair<int?, bool>>();
            for (int i = 0; i < vector.Count; i++)
            {
                if (vectorWithMinimumValue[i].HasValue && !vector[i].Value)
                {
                    if ((vectorWithMinimumValue[i].Value + vectorMinimumValue) > vector[i].Key)
                    {
                        newVector.Add(new KeyValuePair<int?, bool>(vector[i].Key, false));
                    }
                    else
                    {
                        newVector.Add(new KeyValuePair<int?, bool>(vectorWithMinimumValue[i].Value + vectorMinimumValue, false));
                    }
                }
                else
                {
                    newVector.Add(new KeyValuePair<int?, bool>(vector[i].Key, vector[i].Value));
                }
            }

            return newVector;
        }

        private static List<KeyValuePair<int?, bool>> BlockVectorPosition(int position, List<KeyValuePair<int?, bool>> vector)
        {
            vector[position] = new KeyValuePair<int?, bool>(vector[position].Key, true);
            return vector;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static List<List<int?>> BuildMatrix()
        {
            List<List<int?>> matrix = new List<List<int?>>();
            /*******************
            Matrix graphic:
            A--2--B--10--E
            |    /|     /
            |   / |    /
            4  2  4   4
            | /   |  /
            |/    | /
            C--1--D

            Matrix:
            0  2  4  -  -    //From point A
            2  0  2  4  10   //From point B
            4  2  0  1  -    //From point C
            -  4  1  0  4    //From point D
            -  10 -  4  0    //From point E

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

        private static List<KeyValuePair<int?, bool>> BuildResultVector(int vectorPosition, List<List<int?>> matrix)
        {
            List<KeyValuePair<int?, bool>> result = new List<KeyValuePair<int?, bool>>();
            foreach (int? element in matrix[vectorPosition])
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
