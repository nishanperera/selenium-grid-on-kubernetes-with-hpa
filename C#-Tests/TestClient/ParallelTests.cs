using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit;

namespace TestClient
{
    public class ParallelTests
    {
        private string hubUrl = "http://localhost:30001/wd/hub";
        readonly TimeSpan _timeSpan = new TimeSpan(0, 0, 20);
        readonly ChromeOptions _chromeOptions = new ChromeOptions();

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void SmokeTestChrome(int testNumber)
        {
            var driver = new RemoteWebDriver(
                new Uri(hubUrl),
                _chromeOptions.ToCapabilities(),
                _timeSpan
            );
            driver.Navigate().GoToUrl("https://www.bbc.co.uk/");
            driver.PageSource.Should().Contain("BBC");
            driver.Close();
            driver.Quit();
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void SmokeTestChrome2(int testNumber)
        {
            var driver = new RemoteWebDriver(
                new Uri(hubUrl),
                _chromeOptions.ToCapabilities(),
                _timeSpan
            );
            driver.Navigate().GoToUrl("https://www.bbc.co.uk/");
            driver.PageSource.Should().Contain("BBC");
            driver.Close();
            driver.Quit();
        }
    }

    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>();

        public TestDataGenerator()
        {
            for (int i = 1; i < 15; i++)
            {
                _data.Add(new object[] { i });
            }
        }

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}