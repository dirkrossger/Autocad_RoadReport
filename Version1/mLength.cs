using System;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace RoadReport
{
    public static class mLength
    {
        /// <summary>
        ///  http://spiderinnet1.typepad.com/blog/2012/10/autocad-net-getting-curve-length-again.html
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static double GetLength(this Curve curve)
        {
            try
            {
                return Math.Round(curve.GetDistanceAtParameter(curve.EndParam) - curve.GetDistanceAtParameter(curve.StartParam), 3);
            }
            catch
            {
                return double.PositiveInfinity;
            }
        }
    }
}
