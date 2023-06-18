using Akka.Configuration;

namespace DevArt.Core.Akka.Clustering.Configuration;

public class DevArtClusteringDefaultSettings
{
    public static global::Akka.Configuration.Config DefaultConfig()
    {
        return ConfigurationFactory.FromResource<DevArtClusteringDefaultSettings>("DevArt.Core.Akka.Clustering.Configuration.default.conf");
    }
}