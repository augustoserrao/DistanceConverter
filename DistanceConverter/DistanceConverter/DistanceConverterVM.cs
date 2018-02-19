using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter
{
    class DistanceConverterVM : INotifyPropertyChanged
    {
        // CONSTANTS
        const string ERROR_SOURCE_TARGET_UNITS_EQUAL = "FROM and TO units cannot be equal!";
        const string ERROR_SOURCE_UNIT_NOT_SELECTED = "Select FROM unit before converting!";
        const string ERROR_TARGET_UNIT_NOT_SELECTED = "Select TO unit before converting!";

        List<DistanceUnit> distanceUnitList = new List<DistanceUnit>();

        // BINDING VARIABLES
        float sourceValue;
        float convertedValue;
        string sourceUnit = null;
        string targetUnit = null;
        string errorMessage;
        bool errorVisibility = false;

        public float SourceValue
        {
            get { return sourceValue; }
            set { sourceValue = value; _propertyChanged(); }
        }

        public float ConvertedValue
        {
            get { return convertedValue; }
            set { convertedValue = value; _propertyChanged(); }
        }

        public string SourceUnit
        {
            get { return sourceUnit; }
            set { sourceUnit = value; _propertyChanged(); }
        }

        public string TargetUnit
        {
            get { return targetUnit; }
            set { targetUnit = value; _propertyChanged(); }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; _propertyChanged(); }
        }

        public bool ErrorVisibility
        {
            get { return errorVisibility; }
            set { errorVisibility = value; _propertyChanged(); }
        }

        // Class constructor. Gets all units from configuration file
        public DistanceConverterVM()
        {
            DistanceConfigFile distanceConfigFile = new DistanceConfigFile();

            distanceUnitList = distanceConfigFile.GetDistancesUnits();
        }

        // Make all units accessible from outside
        public List<string> GetDistanceUnitsNames()
        {
            List<string> stringList = new List<string>();

            foreach (DistanceUnit distanceUnit in distanceUnitList)
            {
                stringList.Add(distanceUnit.DistanceName);
            }

            return stringList;
        }

        public void Convert()
        {
            float srcConstValue = 0;
            float destConstValue = 0;

            ErrorVisibility = false;

            // Check for errors before converting
            if (SourceUnit == TargetUnit || SourceUnit == null || TargetUnit == null)
            {
                ErrorMessage = SourceUnit == null ? ERROR_SOURCE_UNIT_NOT_SELECTED : TargetUnit == null ? ERROR_TARGET_UNIT_NOT_SELECTED : ERROR_SOURCE_TARGET_UNITS_EQUAL;
                ErrorVisibility = true;
            }
            else
            {
                // Search for units' constants that must be used in convertion calculation
                foreach (DistanceUnit curUnit in distanceUnitList)
                {
                    if (SourceUnit == curUnit.DistanceName)
                    {
                        srcConstValue = curUnit.DistanceValue;
                    }
                    else if (TargetUnit == curUnit.DistanceName)
                    {
                        destConstValue = curUnit.DistanceValue;
                    }
                }

                ConvertedValue = SourceValue * srcConstValue / destConstValue;
            }   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void _propertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
