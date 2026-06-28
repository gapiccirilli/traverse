namespace Traverse.Models.Records.Maps
{
    public record GeocodeResult(
        IEnumerable<Place> Results
    );

    public record Place(
        Coordinate Coordinate,
        DisplayMapRegion DisplayMapRegion,
        string Name,
        StucturedAddress StructuredAddress,
        string Country,
        string CountryCode
    );

    public record DisplayMapRegion(
        double SouthLatitude,
        double WestLongitude,
        double NorthLatitude,
        double EastLongitude
    );

    public record StucturedAddress(
        string AdministrativeArea,
        string AdministrativeAreaCode,
        string Locality,
        string PostalCode,
        string SubLocality,
        string Thoroughfare,
        string SubThoroughfare,
        string FullThoroughfare,
        string[] AreasOfInterest,
        string[] DependentLocalities
    );
}