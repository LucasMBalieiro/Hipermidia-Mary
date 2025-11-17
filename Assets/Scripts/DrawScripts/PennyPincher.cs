using System.Collections.Generic;
using UnityEngine;

public static class PennyPincher
{
    private const int NumResamplePoints = 16;
    
    public static RecognitionResult Recognize(List<Vector2> points)
    {
        if (points.Count == 0)
        {
            return RecognitionResult.NoMatch;
        }
        
        List<Vector2> resampledPoints = Resample(points, NumResamplePoints);
        
        Vector2[] candidateVectors = Vectorize(resampledPoints, false);

        GestureTemplate bestMatch = null;
        float bestScore = float.NegativeInfinity;

        foreach (var template in GameManager.Instance.gestureTemplate)
        {
            float score = 0;
            for (int i = 0; i < candidateVectors.Length; i++)
            {
                score += Vector2.Dot(template.vectors[i], candidateVectors[i]);
            }
            
            if (score > bestScore)
            {
                bestScore = score;
                bestMatch = template;
            }
        }
        
        return bestMatch != null ? new RecognitionResult { gestureName = bestMatch.gestureName, score = bestScore } : RecognitionResult.NoMatch;
    }
    
    public static GestureTemplate CreateTemplate(GestureName gestureName, List<Vector2> points)
    {
        List<Vector2> resampledPoints = Resample(points, NumResamplePoints);
        Vector2[] normalizedVectors = Vectorize(resampledPoints, true); 
        return new GestureTemplate(gestureName, normalizedVectors);
    }
    
    private static Vector2[] Vectorize(List<Vector2> points, bool normalize)
    {
        Vector2[] vectors = new Vector2[points.Count - 1];
        for (int i = 0; i < vectors.Length; i++)
        {
            vectors[i] = points[i + 1] - points[i];
            if (normalize)
            {
                vectors[i].Normalize();
            }
        }
        return vectors;
    }
    
    private static List<Vector2> Resample(List<Vector2> points, int n)
    {
        if (points.Count < 2) return points;

        float interval = PathLength(points) / (n - 1);
        List<Vector2> resampledPoints = new List<Vector2> { points[0] };
        float distanceAccumulator = 0f;

        for (int i = 1; i < points.Count; i++)
        {
            Vector2 p1 = points[i - 1];
            Vector2 p2 = points[i];
            float segmentDistance = Vector2.Distance(p1, p2);

            if (distanceAccumulator + segmentDistance >= interval)
            {
                while (distanceAccumulator + segmentDistance >= interval)
                {
                    float t = (interval - distanceAccumulator) / segmentDistance;
                    Vector2 newPoint = Vector2.Lerp(p1, p2, t);
                    resampledPoints.Add(newPoint);
                    
                    p1 = newPoint;
                    distanceAccumulator = 0;
                    segmentDistance = Vector2.Distance(p1, p2);
                }
                distanceAccumulator = segmentDistance;
            }
            else
            {
                distanceAccumulator += segmentDistance;
            }
        }
        
        while (resampledPoints.Count < n)
        {
            resampledPoints.Add(points[points.Count - 1]);
        }
        while (resampledPoints.Count > n)
        {
            resampledPoints.RemoveAt(resampledPoints.Count - 1);
        }

        return resampledPoints;
    }
    
    private static float PathLength(List<Vector2> points)
    {
        float length = 0;
        for (int i = 1; i < points.Count; i++)
        {
            length += Vector2.Distance(points[i - 1], points[i]);
        }
        return length;
    }
}