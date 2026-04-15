namespace Traverse.Models.Records.Maps
{
    public record GeocodeResult(
        Coordinate Coordinate,
        string Name,
        string AdministrativeArea,
        string AdministrativeAreaCode,
        string Locality,
        string PostalCode,
        string SubLocality,
        string Thoroughfare,
        string SubThoroughfare,
        string FullThoroughfare,
        string[] AreasOfInterest,
        string[] DependentLocalities,
        string Country,
        string CountryCode
    );
}