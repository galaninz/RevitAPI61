using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI61
{
    public class DuctsUtils
    {
        public static List<DuctType> GetDuctTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var ductTypes =
                new FilteredElementCollector(doc)
                .OfClass(typeof(DuctType))
                .Cast<DuctType>()
                .ToList();

            return ductTypes;
        }

        public static List<MechanicalSystemType> GetSystemTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var systemTypes =
                new FilteredElementCollector(doc)
                .OfClass(typeof(MechanicalSystemType))
                .Cast<MechanicalSystemType>()
                .ToList();

            return systemTypes;
        }
    }
}
