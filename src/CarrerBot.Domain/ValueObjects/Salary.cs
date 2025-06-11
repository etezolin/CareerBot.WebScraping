public record Salary(decimal MinValue, decimal MaxValue, string Currency, SalaryPeriod Period)
{
    public static Salary Empty => new Salary(0, 0, "BRL", SalaryPeriod.Month);

    public override string ToString() =>
        $"{Currency} {MinValue:N0} - {MaxValue:N0} / {Period}";
}
