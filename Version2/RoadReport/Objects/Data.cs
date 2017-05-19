using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extensions;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.ComponentModel.DataAnnotations;

namespace RoadReport.Objects
{
    public class Data
    {
        #region ----------------------------------------- Properties
        [Display(AutoGenerateField = false, Name = "ObjectId", Order = 0)]
        public ObjectId ObjectID { get; set; }
        [Display(AutoGenerateField = true, Name = "Id", Order = 0)]
        public string Handle { get { return ObjectID.Handle.ToString(); } }
        [Display(AutoGenerateField = true, Name = "Typ", Order = 1)]
        public string Typ { get; set; }
        [Display(AutoGenerateField = true, Name = "Fläche", Order = 3)]
        public Double Area { get; set; }
        [Display(AutoGenerateField = true, Name = "Länge", Order = 4)]
        public Double Length { get; set; }
        [Display(AutoGenerateField = true, Name = "Layer", Order=2)]
        public string Layer { get; set; }




        #endregion -------------------------------------- Properties


        #region ----------------------------------------- Constructors
        public Data()
        { }

        public Data(Transaction _tr, ObjectId _oid)
        {
            Area = double.PositiveInfinity;
            Length = double.PositiveInfinity;
            ObjectID = _oid;

            try
            {
                using (Entity _ent = (Entity)_tr.GetObject(_oid, OpenMode.ForRead, false, true))
                {
                    Layer = _ent.Layer;
                    Typ = _ent.GetType().Name;
                    if (typeof(Hatch).IsAssignableFrom(_ent.GetType()))
                    {
                        Area = Math.Round(((Hatch)_ent).Area, 3);
                    }
                    else if (typeof(Curve).IsAssignableFrom(_ent.GetType()))
                    {
                        Curve _curve = (Curve)_ent;
                        Area = Math.Round(_curve.Area, 3);
                        Length = Math.Round(_curve.GetDistanceAtParameter(_curve.EndParam) - _curve.GetDistanceAtParameter(_curve.StartParam), 3);
                    }
                }
            }
            catch(SystemException ex) { }
            
        }
        #endregion -------------------------------------- Constructors


        #region ----------------------------------------- private Methods

        #endregion -------------------------------------- private Method


        #region ----------------------------------------- Methods
        public void ZoomTo()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            if (db.Equals(ObjectID.Database))
            {
                Extents3d ext;

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    Entity ent = (Entity)tr.GetObject(ObjectID, OpenMode.ForRead);
                    ext = ent.GeometricExtents;
                }

                ext.TransformBy(ed.CurrentUserCoordinateSystem.Inverse());

                ed.ZoomWin(ext.MinPoint, ext.MaxPoint);
            }
        }
        #endregion -------------------------------------- Method


    }
}
