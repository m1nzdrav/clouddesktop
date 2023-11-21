using System.Collections;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public struct MathfExtension
{
    public static Vector3 GetRotatedVector(Vector3 vectorToRotate, Vector3 rotation, Vector3 coordinatesStartPoint)
    {
        //перевести в новый базис
        /*Vector3 xBasis = new Vector3(1, coordinatesStartPoint.y, coordinatesStartPoint.z);
        Vector3 yBasis = new Vector3(coordinatesStartPoint.x, 1, coordinatesStartPoint.z);
        Vector3 zBasis = new Vector3(coordinatesStartPoint.x, coordinatesStartPoint.y, 1);
        float[,] translationMatrixArray =
        {
            {xBasis.x, yBasis.x, zBasis.x},
            {xBasis.y, yBasis.y, zBasis.y},
            {xBasis.z, yBasis.z, zBasis.z}
        };
        var translationMatrix = Matrix<float>.Build.DenseOfArray(translationMatrixArray);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                stringBuilder.Append($"{translationMatrix[i, j]} \t");
            }
            stringBuilder.Append("\n");
        }
        Debug.Log(stringBuilder.ToString());
        stringBuilder.Clear();
        var translationsVector = Vector<float>.Build.DenseOfArray(new float[]{vectorToRotate.x, vectorToRotate.y, vectorToRotate.z});
        translationsVector = translationMatrix.Inverse() * translationsVector;
        vectorToRotate = new Vector3(translationsVector[0], translationsVector[1], translationsVector[2]);*/
        //повернуть
        float[,] rotationMatrixArray = GetYRotationMatrix(30);
        var rotationMatrix = Matrix<float>.Build.DenseOfArray(rotationMatrixArray);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                stringBuilder.Append($"{rotationMatrix[i, j]} \t");
            }
            stringBuilder.Append("\n");
        }
        Debug.Log(stringBuilder.ToString());
        stringBuilder.Clear();
        float[] vectorToRotateArray = { vectorToRotate.x, vectorToRotate.y, vectorToRotate.z};
        var vectorToRotateVector = Vector<float>.Build.DenseOfArray(vectorToRotateArray);
        var rotatedVector = vectorToRotateVector * rotationMatrix;
        vectorToRotate = new Vector3(rotatedVector[0], rotatedVector[1], rotatedVector[2]);
        /*var rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(rotation.x, rotation.y, rotation.z));
        vectorToRotate = rotationMatrix.MultiplyPoint3x4(vectorToRotate);*/
        //вернуть в старый базис
        /*translationsVector = Vector<float>.Build.DenseOfArray(new float[] { vectorToRotate.x, vectorToRotate.y, vectorToRotate.z });
        translationsVector = translationMatrix * translationsVector;
        vectorToRotate = new Vector3(translationsVector[0], translationsVector[1], translationsVector[2]);*/
        return vectorToRotate;
        /*float[,] rotationMatrixArray = 
        {
            { Mathf.Cos(y) * Mathf.Cos(z), -Mathf.Sin(z) * Mathf.Cos(y), Mathf.Sin(y) },
            {
                Mathf.Sin(x) * Mathf.Sin(y) * Mathf.Cos(z) + Mathf.Sin(z) * Mathf.Cos(x),
                -Mathf.Sin(x) * Mathf.Sin(y) * Mathf.Sin(z) + Mathf.Cos(x) * Mathf.Cos(z), -Mathf.Sin(x) * Mathf.Cos(y)
            },
            { Mathf.Sin(x) * Mathf.Sin(z) - Mathf.Sin(y) * Mathf.Cos(x) * Mathf.Cos(z), Mathf.Sin(x) * Mathf.Cos(z) + Mathf.Sin(y) * Mathf.Sin(z) * Mathf.Cos(x), Mathf.Cos(x) * Mathf.Cos(y)}
        };*/
        /*float[,] rotationMatrixArray = 
        {
            { Mathf.Cos(z) * Mathf.Cos(y), Mathf.Cos(z) * Mathf.Sin(y) * Mathf.Sin(x) - Mathf.Sin(z) * Mathf.Cos(x), Mathf.Cos(z) * Mathf.Sin(y) * Mathf.Cos(x) + Mathf.Sin(z) * Mathf.Sin(x) },
            {
                Mathf.Sin(z) * Mathf.Cos(y), Mathf.Sin(z) * Mathf.Sin(y) * Mathf.Sin(x) + Mathf.Cos(z) * Mathf.Cos(x), Mathf.Sin(z) * Mathf.Sin(y) * Mathf.Cos(x) - Mathf.Cos(z) * Mathf.Sin(x)  
            },
            { -Mathf.Sin(y), Mathf.Cos(y) * Mathf.Sin(x), Mathf.Cos(y) * Mathf.Cos(x) }
        };
        int rows = rotationMatrixArray.GetUpperBound(0) + 1;
        int columns = rotationMatrixArray.Length / rows; 
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                stringBuilder.Append($"{rotationMatrixArray[i, j]} \t");
            }
            stringBuilder.Append("\n");
        }
        Debug.Log(stringBuilder.ToString());*/
        /*float[,] rotationMatrixArray =
        {
            {0.492404f,	  -0.403317f,	  0.771281f},
            {0.413176f,	  0.888258f,	  0.200706f,},
            {-0.766044f,	  0.219846f,	  0.604023f,}
        };
        var rotationMatrix = Matrix<float>.Build.DenseOfArray(rotationMatrixArray);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                stringBuilder.Append($"{rotationMatrix[i, j]} \t");
            }
            stringBuilder.Append("\n");
        }
        Debug.Log(stringBuilder.ToString());
        float[] vectorToRotateArray = { vectorToRotate.x, vectorToRotate.y, vectorToRotate.z};
        var vectorToRotateVector = Vector<float>.Build.DenseOfArray(vectorToRotateArray);
        var rotatedVector = vectorToRotateVector * rotationMatrix;
        return new Vector3(rotatedVector[0], rotatedVector[1], rotatedVector[2]);*/
        
        return Vector3.zero;
    }

    public static float[,] GetYRotationMatrix(float angel)
    {
        return new float[,]
        {
            { Mathf.Cos(angel), 0, -Mathf.Sin(angel)},
            { 0, 1, 0 },
            { Mathf.Sin(angel), 0, Mathf.Cos(angel) }
        };
    }
}
