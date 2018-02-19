namespace DistanceConverter
{
    // Class that works as typedef struct in C
    class DistanceUnit
    {
        public string DistanceName;
        public float DistanceValue;

        public DistanceUnit(string distanceName, float distanceValue)
        {
            this.DistanceName = distanceName;
            this.DistanceValue = distanceValue;
        }
    }
}
