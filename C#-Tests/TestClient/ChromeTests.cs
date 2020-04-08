using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit;

namespace TestClient
{
    public class ChromeTests
    {
        private readonly IWebDriver _driver;

        public ChromeTests()
        {
            var hubUrl = "http://localhost:30001/wd/hub";
            var timeSpan = new TimeSpan(0, 0, 20);
            var chromeOptions = new ChromeOptions();
            _driver = new RemoteWebDriver(
                new Uri(hubUrl),
                chromeOptions.ToCapabilities(),
                timeSpan
            );
        }


        [Fact]
        public void SmokeTestChromeSingle()
        {
            _driver.Navigate().GoToUrl("https://www.google.com");
            _driver.PageSource.Should().Contain("google");
        }

        public void Dispose()
        {
            _driver.Dispose();
        }

    }
}
