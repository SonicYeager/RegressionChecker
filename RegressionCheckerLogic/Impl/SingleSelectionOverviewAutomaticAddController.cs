using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class SingleSelectionOverviewAutomaticAddController : ISingleSelectionOverviewAutomaticAddController
    {
        public ISingleSelectionOverviewAutomaticAddUI SingleSelectionOverviewAutomaticAdd { get; set; }
        public IDataConverter DataConverter { get; set; }
        public ICSVFileReader CSVFileReader { get; set; }
        public IExternalProgrammLauncher ExternalProgrammLauncher { get; set; }

        public SingleSelectionOverviewAutomaticAddController(IDataConverter dataConverter, ICSVFileReader csvFileReader, IExternalProgrammLauncher externalProgrammLauncher,ISingleSelectionOverviewAutomaticAddUI singleSelectionOverviewAutomaticAddUI)
        {
            DataConverter = dataConverter;
            CSVFileReader = csvFileReader;
            ExternalProgrammLauncher = externalProgrammLauncher;
            SingleSelectionOverviewAutomaticAdd = singleSelectionOverviewAutomaticAddUI;
        }

        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethodEntries)
        {
            SingleSelectionOverviewAutomaticAdd.SetRegressiveMethods(regressiveMethodEntries);
        }
    }
}
