using System;
using UnityEngine;

namespace FusionUnityExtras.Runtime.Debugging
{
    public enum LineType
    {
        Line, 
        EndWithArrow, StartWithArrow, DoubleArrow, 
        Dotted,
        DottedEndWithArrow, DottedStartWithArrow, DottedDoubleArrow
    }
    
    public static class ExtraGizmos
    {
        public static void DrawSpline(Vector3 from, Vector3 to, Vector3 controlPoint, LineType lineType,
            int segments = 20, float arrowHeadLength = 0.2f, float arrowHeadAngle = 20.0f)
        {
            DrawSplineLine(from, to, controlPoint, segments, 
                lineType is LineType.Dotted or LineType.DottedDoubleArrow or LineType.DottedEndWithArrow or LineType.DottedStartWithArrow,
                lineType is LineType.StartWithArrow or LineType.DottedStartWithArrow or LineType.DoubleArrow or LineType.DottedDoubleArrow,
                lineType is LineType.EndWithArrow or LineType.DottedEndWithArrow or LineType.DoubleArrow or LineType.DottedDoubleArrow,
                arrowHeadLength, arrowHeadAngle
            );
        }

        private static void DrawSplineLine(Vector3 from, Vector3 to, Vector3 controlPoint, int segments, bool dotted = false, bool startWithArrow = false, bool endWithArrow = false, float arrowHeadLength = 0.2f, float arrowHeadAngle = 20.0f)
        {
            Vector3 previousPoint = from;

            for (int i = 1; i <= segments; i++)
            {
                float t = i / (float)segments;
                Vector3 currentPoint = CalculateQuadraticBezierPoint(t, from, controlPoint, to);
                if (i % 2 == 0 || !dotted) Gizmos.DrawLine(previousPoint, currentPoint);
                
                var lastDirection = (currentPoint - previousPoint).normalized;

                if (i == segments && endWithArrow)
                {
                    // When reaching the last segment, we stop drawing the line and add the arrowhead
                    DrawArrowHead(previousPoint, to, lastDirection, arrowHeadLength, arrowHeadAngle);
                } 
                else if (i == 1 && startWithArrow)
                {
                    DrawArrowHead(currentPoint, previousPoint, -lastDirection, arrowHeadLength, arrowHeadAngle);
                }
                
                previousPoint = currentPoint;
            }
        }

        private static void DrawArrowHead(Vector3 from, Vector3 to, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            // Calculate the right and left lines of the arrowhead
            Vector3 right = Vector3.zero;
            Vector3 left = Vector3.zero;

            if (direction.magnitude > Mathf.Epsilon)
            {
                right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back;
                left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back;
            }
            
            // Draw the arrowhead lines at the end of the spline
            Gizmos.DrawLine(to, to + right * arrowHeadLength);
            Gizmos.DrawLine(to, to + left * arrowHeadLength);
        }
        
        // Dark magic
        private static Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            return u * u * p0 + 2 * u * t * p1 + t * t * p2;
        }
    }
}
