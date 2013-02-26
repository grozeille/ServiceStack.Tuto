using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using ServiceStack.Tuto.Rest.Common.Operations;

namespace ServiceStack.Tuto.Rest.Common.Validators
{
    public class QuoteUpdateReqValidator : AbstractValidator<QuoteUpdateReq>
    {
        public QuoteUpdateReqValidator()
        {
            RuleFor(x => x.Value).GreaterThan(0.0).WithMessage("Should be greater than zero");
        }
    }
}
