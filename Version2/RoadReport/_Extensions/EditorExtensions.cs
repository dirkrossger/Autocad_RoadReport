using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace Extensions
{
    public static class EditorExtensions
    {
        public static void ZoomWin(this Editor ed, Point3d min, Point3d max)
        {
            Point2d min2d = new Point2d(min.X, min.Y);
            Point2d max2d = new Point2d(max.X, max.Y);

            ed.ZoomWin(min2d, max2d);
        }

        public static void ZoomWin(this Editor ed, Point2d min, Point2d max)
        {
            Point2d min2d = new Point2d(min.X, min.Y);
            Point2d max2d = new Point2d(max.X, max.Y);

            ViewTableRecord view = new ViewTableRecord();

            view.CenterPoint = min2d + ((max2d - min2d) / 2.0);
            view.Height = max2d.Y - min2d.Y;
            view.Width = max2d.X - min2d.X;
            ed.SetCurrentView(view);
        }
    }
}
