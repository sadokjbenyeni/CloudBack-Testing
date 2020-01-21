using EventFlow.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudBacktesting.PaymentService.Domain.Specifications
{
    public class IsNumberValidSpecification : Specification<string>
    {
        public enum CreditCardTypeType
        {
            Visa,
            MasterCard,
            Discover,
            Amex,
        }

        private const string cardRegex = "^(?:(?<Visa>4\\d{3})|(?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>(?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)(?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})|(?:\\d{4}\\1\\d{4}\\1\\d{4})))$";

        public bool IsSatisfiedBy(string cardNumber, CreditCardTypeType? cardType)
        {
            PassesLuhenTestSpecification passesLuhen = new PassesLuhenTestSpecification();
            Regex cardTest = new Regex(cardRegex);

            if (cardTest.Match(cardNumber).Groups[cardType.ToString()].Success)
            {
                if (passesLuhen.IsSatisfiedBy(cardNumber))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        protected override IEnumerable<string> IsNotSatisfiedBecause(string cardNumber)
        {
            if (IsSatisfiedBy(cardNumber) is false)
            {
                yield return string.Format("{0} is not a valid card type", cardNumber);
            }
        }
    }
}
