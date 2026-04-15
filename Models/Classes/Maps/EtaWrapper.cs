namespace Traverse.Models.Records.Maps
{
    public class EtaWrapper
    {
        public long EventId { get; set; }
        public IEnumerable<EtaResult> Etas { get; set; }

        public EtaWrapper()
        {
            Etas = [];
        }
    }
}