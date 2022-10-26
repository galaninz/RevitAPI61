using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI61
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public List<DuctType> DuctTypes { get; } = new List<DuctType>();
        public List<MechanicalSystemType> DuctSystemTypes { get; } = new List<MechanicalSystemType>();
        public List<Level> Levels { get; } = new List<Level>();
        public DelegateCommand SaveCommand { get; }
        public double Offset { get; set; }
        public List<XYZ> Points { get; } = new List<XYZ>();
        public MechanicalSystemType SelectedDuctSystemType { get; set; }
        public DuctType SelectedDuctType { get; set; }
        public Level SelectedLevel { get; set; }


        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            DuctTypes = DuctsUtils.GetDuctTypes(commandData);
            DuctSystemTypes = DuctsUtils.GetSystemTypes(commandData);
            Levels = LevelsUtils.GetLevels(commandData);
            SaveCommand = new DelegateCommand(OnSaveCommand);
            Offset = 900;
            Points = SelectionUtils.GetPoints(_commandData, "Выберите точки", ObjectSnapTypes.Endpoints);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (Points.Count < 2 || SelectedDuctType == null || SelectedLevel == null)
                return;

            var points = new List<Point>();


            var startPoint = new XYZ(Points[0].X, Points[0].Y, Points[0].Z + Offset);
            var endPoint = new XYZ(Points[1].X, Points[1].Y, Points[1].Z + Offset);

            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0)
                    continue;
            }

            using (var ts = new Transaction(doc, "Create duct"))
            {
                ts.Start();
                Duct.CreatePlaceholder(doc, SelectedDuctSystemType.Id, SelectedDuctType.Id, SelectedLevel.Id, startPoint, endPoint);
                ts.Commit();
            }

            RaiseCloseRequest();

        }

        public event EventHandler CloseRequest;

        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
