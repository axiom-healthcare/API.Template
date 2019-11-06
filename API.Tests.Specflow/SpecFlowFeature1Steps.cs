using System;
using TechTalk.SpecFlow;
using Example;
using Xunit;
using FluentAssertions;

namespace API.Tests.Specflow
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        private Calculator calculator = new Calculator();
        private int results;

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            calculator.FirstNumber = number;
        }
        
        [Given(@"I have also entered (.*) into the calculator")]
        public void GivenIHaveAlsoEnteredIntoTheCalculator(int number)
        {
            calculator.SecondNumber = number;
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            results = calculator.Add(); 
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int result)
        {
            results.Should().Be(result);
        }
    }
}
