namespace Traverse.Models.Records.Maps
{
    public class EtaWrapper
    {
        public long FromEventId { get; set; }
        public long ToEventId { get; set; }
        public IEnumerable<EtaResult> Etas { get; set; }

        public EtaWrapper()
        {
            Etas = [];
        }
    }
}