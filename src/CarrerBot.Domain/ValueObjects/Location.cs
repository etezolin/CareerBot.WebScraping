public record Location(string City, string State, string Country)
{
    public static Location Empty => new Location("", "", "");

    public override string ToString() => $"{City}, {State}, {Country}";

    public bool IsEmpty => string.IsNullOrEmpty(City) &&
                          string.IsNullOrEmpty(State) &&
                          string.IsNullOrEmpty(Country);
}
