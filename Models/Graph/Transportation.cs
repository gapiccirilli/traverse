namespace Traverse.Models.Graph
{
    public class Transportation
    {
        public long FromEventId { get; set; }
        public long ToEventId { get; set; }
        public double WeightSeconds { get; set; }
        public TransportMode TransportMode { get; set; }
        public double Distance { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
        public TimeSpan Duration { get; set; }
    }
}