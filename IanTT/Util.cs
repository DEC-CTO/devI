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
    }
}
