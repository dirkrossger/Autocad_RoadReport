using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace RoadReport
{
    public static class mArea
    {
        public static double GetArea(Entity ent)
        {
            try
            {
                if (ent.GetType().Name == "Hatch")
                {
                    Hatch h = ent as Hatch;
                    return Math.Round(h.Area, 3);
                }
                else
                {
                    if (ent is Curve)
                    {
                        Curve c = (Curve)ent;
                        return Math.Round(c.Area, 3);
                    }
                    else
                    {
                        Curve curve = (Curve)ent;
                        var x = curve.GetGeCurve();
                        x.GetArea(curve.EndParam, curve.StartParam);

                        return Math.Round(x.GetArea(curve.EndParam, curve.StartParam), 3);
                    }
                }
            }
            catch
            {
                return double.PositiveInfinity;
            }
            
        }

    }
}
