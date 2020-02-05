using CloudBacktesting.PaymentService.Domain.Specifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudBacktesting.PaymentService.Domain.Tests
{
    [TestFixture]
    public class CreditCardTests
    {
        [TestCase("4111 1111 1111 1111")]
        [TestCase("5500 0000 0000 0004")]
        [TestCase("3400 0000 0000 009")]
        [TestCase("6011 0000 0000 0004")]
        [TestCase("3088 0000 0000 0009")]
        public void Should_be_valid_following_to_luhn_algorithm(string cardNumber)
        {
            var isPassLuhen = new PassesLuhenTestSpecification();
            Assert.IsTrue(isPassLuhen.IsSatisfiedBy(cardNumber));
        }

        [TestCase("4111 1111 1111 1111", "Visa")]
        [TestCase("5500 0000 0000 0004", "MasterCard")]
        [TestCase("6011 0000 0000 0004", "Discover")]
        [TestCase("3782 822463 10005", "Amex")]
        public void Should_have_the_correct_card_type(string cardNumber, string cardType)
        {
            var cardTypeVerifier = new GetCardTypeFromNumber();
            Assert.AreEqual(cardTypeVerifier.GetCardType(cardNumber).Value.ToString(), cardType);
        }
    }
}
