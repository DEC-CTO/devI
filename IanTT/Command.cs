using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Structure;
using System.Diagnostics;

namespace IanTT
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            #region 깃연습 함수연습 선택연습
            //깃연습입니다. 연습중......
            //Reference r = uiDoc.Selection.PickObject
            //    (ObjectType.Element, "객체를 선택하세요");
            //Element e = doc.GetElement(r);

            //IList<Reference> refs = uiDoc.Selection.PickObjects
            //    (ObjectType.Face, "객체들을 선택하세요");

            //Face face = doc.GetElement(refs[0]).
            //    GetGeometryObjectFromReference(refs[0]) as Face;

            //List<Curve> dd = Util.GetCurvesFromFace(face);

            //List<Curve> curves = new List<Curve>();
            //foreach (Reference item in refs)
            //{
            //    Edge edge = doc.GetElement(item).
            //        GetGeometryObjectFromReference(item) as Edge;
            //    Curve c = edge.AsCurve();
            //    curves.Add(c);
            //}

            //FamilySymbol tt = Util.GetFamilySymbolByName("G1", doc);
            //if(tt == null)
            //{
            //    Autodesk.Revit.UI.TaskDialog.Show("오류", "해당이름의 패밀리심볼을 찾지 못했습니다.");
            //    return Result.Failed;
            //}
            //Level level = doc.ActiveView.GenLevel;
            //foreach (Curve c in dd)
            //{
            //    using (Transaction trans = new Transaction(doc, "Create Beam"))
            //    {
            //        trans.Start();
            //        tt.Activate();
            //        FamilyInstance fi = doc.Create.NewFamilyInstance
            //            (c, tt, level, StructuralType.Beam);
            //        trans.Commit();
            //    }
            //}

            //foreach (Reference r in refs)
            //{
            //    Element e = doc.GetElement(r);
            //    Wall wall = e as Wall;
            //    Parameter param = wall.get_Parameter
            //        (BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
            //    Parameter param2 = wall.LookupParameter("마크");

            //    using(Transaction trans = new Transaction(doc, "Set Comments"))
            //    {
            //        trans.Start();
            //        param.Set("IanTT");
            //        //param2.Set("123");
            //        trans.Commit();
            //    }
            //    string a = param.AsString();
            //    Autodesk.Revit.UI.TaskDialog.Show("코멘트의 정보는 : ", a);

            //}
            #endregion

            OpenFileDialog ofd = new OpenFileDialog();
            string filePath = "";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }

            List<XYZ> points = new List<XYZ>();
            using(StreamReader sr = new StreamReader(filePath))
            {
                string line = "";
                while((line = sr.ReadLine()) != null)
                {
                    string[] strs = line.Split(',');
                    double x = Convert.ToDouble(strs[0]);
                    double y = Convert.ToDouble(strs[1]);
                    double z = Convert.ToDouble(strs[2]);
                    XYZ p1 = new XYZ(x, y, z)/304.8;
                    points.Add(p1);
                }
            }

            List<Curve> curves = Util.GetCurveListFromPts(points);
            FamilySymbol fs = Util.GetFamilySymbolByName("G1", doc);
            Level level = doc.ActiveView.GenLevel;
            Util.CreateFamilyInstanceFromCurve(curves, fs, level, doc);
            return Result.Succeeded;
        }
    }
}
