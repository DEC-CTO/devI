using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Structure;

namespace IanTT
{
    public class Util
    {
        /// <summary>
        /// 선택한 Face의 Edge를 Curve로 반환하는 함수
        /// </summary>
        /// <param name="레빗에서 선택한 Face"></param>
        /// <returns></returns>
        public static List<Curve> GetCurvesFromFace(Face face)
        {
            List<Curve> curves = new List<Curve>();
            EdgeArrayArray edgeArrays = face.EdgeLoops;
            foreach (EdgeArray edgeArray in edgeArrays)
            {
                foreach (Edge edge in edgeArray)
                {
                    Curve c = edge.AsCurve();
                    curves.Add(c);
                }
            }
            return curves;
        }

        /// <summary>
        /// 이름으로 패밀리심볼을 찾는다.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static FamilySymbol GetFamilySymbolByName(string name, Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_StructuralFraming);
            collector.OfClass(typeof(FamilySymbol));
            FamilySymbol fs = null;

            foreach (FamilySymbol item in collector)
            {
                if (name == item.Name)
                {
                    fs = item;
                    break;
                }
            }
            return fs;
        }

        /// <summary>
        /// XYZ 좌표 리스트를 받아서 Curve 리스트로 반환하는 함수
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Curve> GetCurveListFromPts(List<XYZ> points)
        {
            List<Curve> curves = new List<Curve>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                Line line = Line.CreateBound(points[i], points[i + 1]);
                curves.Add(line);
            }

            return curves;
        }


        public static void CreateFamilyInstanceFromCurve
            (List<Curve> c, FamilySymbol fs, Level level, Document doc)
        {
            foreach (Curve item in c)
            {
                using(Transaction trans = new Transaction(doc, "Create Beam"))
                {
                    trans.Start();
                    fs.Activate();
                    FamilyInstance fi = doc.Create.NewFamilyInstance
                        (item, fs, level, StructuralType.Beam);
                    trans.Commit();
                }
            }
        }

        /// <summary>
        /// XYZ 좌표 리스트를 받아서 CurveLoop로 반환하는 함수
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>

        public static CurveLoop GetCurveLoopFormPts(List<XYZ> points)
        {
            CurveLoop cl = new CurveLoop();

            for(int i = 0; i < points.Count; i++)
            {
                if(i < points.Count - 1)
                {
                    Line line = Line.CreateBound(points[i], points[i + 1]);
                    cl.Append(line);
                }
                else if(i == points.Count - 1)
                {
                    Line line = Line.CreateBound(points[i], points[0]);
                    cl.Append(line);
                }                        
            }
            return cl;
        }

        public static void CreateFloor
            (Document doc, IList<CurveLoop> cl, ElementId floorid, ElementId levelid, double tt)
        {
            using(Transaction trans = new Transaction(doc, "바닥을 생성합니다."))
            {
                trans.Start();
                Floor f = Floor.Create(doc, cl, floorid, levelid);
                Parameter param = f.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                if (param == null)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("경고", "값이 없어?");
                }
                param.Set(tt);
                trans.Commit();
            }
        }

        public static FloorType FindFloorTypeByName(Document doc, string name)
        {
            FloorType ft = null;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Floors);
            col.OfClass(typeof(FloorType));

            foreach (FloorType item in col)
            {
                if(name == item.Name)
                {
                    ft = item;
                    break;
                }
            }

            return ft;
        }
    }
}
