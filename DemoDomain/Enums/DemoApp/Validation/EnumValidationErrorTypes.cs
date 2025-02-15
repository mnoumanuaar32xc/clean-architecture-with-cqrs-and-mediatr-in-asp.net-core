using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Enums.DemoApp.Validation
{

    public enum EnumValidationErrorTypes
    {
        NotEmptyValidator,
        NotNullValidator,
        EmptyValidator,
        NullValidator,
        LessThanValidator,
        LessThanOrEqualValidator,
        GreaterThanValidator,
        GreaterThanOrEqualValidator,
        EqualValidator,
        NotEqualValidator,
        ExclusiveBetweenValidator,
        InclusiveBetweenValidator,
        IsNumericTypeValidator,
        ExactLengthValidator,
        MinimumLengthValidator,
        MaximumLengthValidator,
        CreditCardValidator,
        EmailValidator,
        EnumValidator,
        HaveAtLeastOneValue,

        MustValidator,
        MatchesValidator,
        ContainsValidator,
        NotContainsValidator,
        CountValidator,
        MinimumCountValidator,
        MaximumCountValidator,
        RuleForEachValidator,
        WhenValidator,
        UnlessValidator,
        DependentRulesValidator,
        ScalePrecisionValidator,
        IsoDateFormatValidator
    }
}
