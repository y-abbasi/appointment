using System;

namespace Suzianna.Reporting.Exceptions;

public class FeatureNotFoundException : Exception
{
    public FeatureNotFoundException(string featureTitle)
        : base(string.Format(ExceptionMessages.FeatureNotFound, featureTitle))
    {
        FeatureTitle = featureTitle;
    }

    public string FeatureTitle { get; }
}