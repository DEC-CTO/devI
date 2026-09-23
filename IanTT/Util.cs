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
                if(name == item.Name)
                {
                    fs = item;
                    break;
                }
            }
            return fs;
        }


    }
}
