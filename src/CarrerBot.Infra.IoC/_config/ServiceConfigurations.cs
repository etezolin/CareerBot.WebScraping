namespace CarrerBot.Infra.IoC;

public class ServiceConfigurations
{
    public int SecondsTimeout { get; set; } = 1;
    public int SystemConfigId { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public int SecondsDelay { get; set; }
}

public class ConnectionStrings
{
    public SqlConfigurations Sql { get; set; }
}

public class SqlConfigurations
{
    public string DataSystem { get; set; }
}
