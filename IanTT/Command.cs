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

            #region 텍스트파일로 보 생성하기
            //OpenFileDialog ofd = new OpenFileDialog();
            //string filePath = "";
            //if(ofd.ShowDialog() == DialogResult.OK)
            //{
            //    filePath = ofd.FileName;
            //}

            //List<XYZ> points = new List<XYZ>();
            //using(StreamReader sr = new StreamReader(filePath))
            //{
            //    string line = "";
            //    while((line = sr.ReadLine()) != null)
            //    {
            //        string[] strs = line.Split(',');
            //        double x = Convert.ToDouble(strs[0]);
            //        double y = Convert.ToDouble(strs[1]);
            //        double z = Convert.ToDouble(strs[2]);
            //        XYZ p1 = new XYZ(x, y, z)/304.8;
            //        points.Add(p1);
            //    }
            //}

            //List<Curve> curves = Util.GetCurveListFromPts(points);
            //FamilySymbol fs = Util.GetFamilySymbolByName("G1", doc);
            //Level level = doc.ActiveView.GenLevel;
            //Util.CreateFamilyInstanceFromCurve(curves, fs, level, doc);
            #endregion


            //XYZ p1 = new XYZ(0, 0, 0);
            //XYZ p2 = new XYZ(5000, 0, 0)/304.8;
            //XYZ p3 = new XYZ(5000, 5000, 0) / 304.8;
            //XYZ p4 = new XYZ(0, 5000, 0) / 304.8;

            //List<XYZ> points = new List<XYZ>();
            //points.Add(p1);
            //points.Add(p2);
            //points.Add(p3);
            //points.Add(p4);

            //XYZ pp1 = new XYZ(1000, 1000, 0) / 304.8;
            //XYZ pp2 = new XYZ(4000, 1000, 0) / 304.8;
            //XYZ pp3 = new XYZ(4000, 4000, 0) / 304.8;
            //XYZ pp4 = new XYZ(1000, 4000, 0) / 304.8;

            //List<XYZ> points1 = new List<XYZ>();
            //points1.Add(pp1);
            //points1.Add(pp2);
            //points1.Add(pp3);
            //points1.Add(pp4);

            //XYZ ppp1 = new XYZ(6000, 0, 0) / 304.8;
            //XYZ ppp2 = new XYZ(10000, 0, 0) / 304.8;
            //XYZ ppp3 = new XYZ(10000, 4000, 0) / 304.8;
            //XYZ ppp4 = new XYZ(6000, 4000, 0) / 304.8;

            //List<XYZ> points2 = new List<XYZ>();
            //points2.Add(ppp1);
            //points2.Add(ppp2);
            //points2.Add(ppp3);
            //points2.Add(ppp4);


            //List<IList<CurveLoop>> loops = new List<IList<CurveLoop>>();

            //IList<CurveLoop> cls = new List<CurveLoop>();
            //CurveLoop lp = Util.GetCurveLoopFormPts(points);
            //cls.Add(lp);
            //CurveLoop lp1 = Util.GetCurveLoopFormPts(points1);
            //cls.Add(lp1);

            //IList<CurveLoop> cls1 = new List<CurveLoop>();
            //CurveLoop lp2 = Util.GetCurveLoopFormPts(points2);
            //cls1.Add(lp2);

            //loops.Add(cls);
            //loops.Add(cls1);

            //foreach (IList<CurveLoop> item in loops)
            //{
            //    Debug.Print(item.Count.ToString());
            //}

            List<floor> fl = FloorATT.GetFloorData(doc, uiDoc);

            foreach(floor f in fl)
            {
                Util.CreateFloor
                    (doc, f.m_CurveLoops, f.m_FloorType, f.m_Level, f.m_FloorTypeTHK);
            }

            return Result.Succeeded;
        }
    }
}
