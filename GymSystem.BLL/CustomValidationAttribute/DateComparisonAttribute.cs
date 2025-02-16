using System;
using System.ComponentModel.DataAnnotations;

public class DateComparisonAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateComparisonAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDate = (DateTime)value;
        var endDateProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (endDateProperty == null)
            throw new ArgumentException($"Unknown property: {_comparisonProperty}");

        var endDate = (DateTime)endDateProperty.GetValue(validationContext.ObjectInstance);

        if (startDate >= endDate)
            return new ValidationResult($"Start date must be before end date");

        return ValidationResult.Success;
    }
}