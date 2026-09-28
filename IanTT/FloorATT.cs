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
using System.Diagnostics;

namespace IanTT
{
    public class FloorATT
    {
        public static List<floor> GetFloorData(Document doc, UIDocument uiDoc)
        {
            List<floor> floors = new List<floor>();

            IList<Reference> refs = uiDoc.Selection.PickObjects
                            (ObjectType.Face, "구조바닥을 선택하세요");
            Element e1 = doc.GetElement(refs[0]);
            Floor levelFloor = e1 as Floor;
            Parameter levelparam = levelFloor.get_Parameter
                        (BuiltInParameter.LEVEL_PARAM);
            ElementId level = levelparam.AsElementId();

            foreach (Reference item in refs)
            {
                floor fclass = new floor();
                fclass.m_Level = level;
                Element e = doc.GetElement(item);
                Face f = e.GetGeometryObjectFromReference(item) as Face;
                EdgeArrayArray eaa = f.EdgeLoops;
                IList<CurveLoop> cls = new List<CurveLoop>();
                foreach (EdgeArray ea in eaa)
                {
                    CurveLoop cl = new CurveLoop();
                    foreach (Edge ed in ea)
                    {
                        Curve c = ed.AsCurveFollowingFace(f);
                        cl.Append(c);
                    }
                    cls.Add(cl);
                }
                fclass.m_CurveLoops = cls;
                FloorType ft = Util.FindFloorTypeByName(doc, "화강석");
                fclass.m_FloorType = ft.Id;

                Parameter param = ft.get_Parameter
                (BuiltInParameter.FLOOR_ATTR_DEFAULT_THICKNESS_PARAM);
                if (param == null)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("경고", "값이 없어?");
                }
                double t = param.AsDouble();
                fclass.m_FloorTypeTHK = t;

                floors.Add(fclass);
            }

            return floors;
        }
    }

    public class floor
    {
        public ElementId m_Level { get; set; }
        public ElementId m_FloorType { get; set; }
        public IList<CurveLoop> m_CurveLoops { get; set; }
        public double m_FloorTypeTHK { get; set; }
    }
}
