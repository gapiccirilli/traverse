namespace Traverse.Models.Records.Maps
{
    public class EtaResult
    {
        public long FromEventId { get; set; }
        public long ToEventId { get; set; }
        public IEnumerable<Eta> Etas { get; set; }

        public EtaResult()
        {
            Etas = [];
        }
    }
}