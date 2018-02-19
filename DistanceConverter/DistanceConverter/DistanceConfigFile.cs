

using System;
using System.Collections.Generic;
using System.IO;

namespace DistanceConverter
{
    class DistanceConfigFile
    {
        const string DIS_CONFIG_FILE_NAME = "distance.cfg";

        // Standard values
        const string DIS_UNIT_NAME_YARDS = "Yards";
        const float DIS_UNIT_VALUE_YARDS = 36f;
        const string DIS_UNIT_NAME_FEET = "Feet";
        const float DIS_UNIT_VALUE_FEET = 12f;
        const string DIS_UNIT_NAME_INCHES = "Inches";
        const float DIS_UNIT_VALUE_INCHES = 1f;

        // Gets all distance units from file. If file doesn't exist, creates a new one with standard units
        public List<DistanceUnit> GetDistancesUnits()
        {
            List<DistanceUnit> retList = new List<DistanceUnit>();
            
            if (!File.Exists(DIS_CONFIG_FILE_NAME))
            {
                var curFile = File.Create(DIS_CONFIG_FILE_NAME);
                curFile.Close();

                retList.Add(new DistanceUnit(DIS_UNIT_NAME_YARDS, DIS_UNIT_VALUE_YARDS));
                retList.Add(new DistanceUnit(DIS_UNIT_NAME_FEET, DIS_UNIT_VALUE_FEET));
                retList.Add(new DistanceUnit(DIS_UNIT_NAME_INCHES, DIS_UNIT_VALUE_INCHES));

                foreach (DistanceUnit curDistanceUnit in retList)
                {
                    AddNewUnitToFile(curDistanceUnit);
                }
            }
            else
            {
                string[] fileLines = File.ReadAllLines(DIS_CONFIG_FILE_NAME);

                foreach (string fileLine in fileLines)
                {
                    string[] lineSplited = fileLine.Split(';');
                    retList.Add(new DistanceUnit(lineSplited[0], float.Parse(lineSplited[1])));
                }
            }

            return retList;
        }

        public void AddNewUnitToFile(DistanceUnit distanceUnit)
        {
            File.AppendAllText(DIS_CONFIG_FILE_NAME, $"{distanceUnit.DistanceName};{distanceUnit.DistanceValue}\n");
        }
    }
}
