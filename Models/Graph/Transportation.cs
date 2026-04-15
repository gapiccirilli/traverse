namespace Traverse.Models.Graph
{
    public class Transportation
    {
        public long Id { get; set; }
        public long FromEventId { get; set; }
        public long ToEventId { get; set; }
        public double WeightMinutes { get; set; }
        public TransportMode TransportMode { get; set; }
        public double Distance { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
        public TimeSpan Duration { get; set; }
    }
}