using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace BMICalculatorSeleniumTests
{
    [TestClass]
    public class seleniumtests
    {
        private static TestContext testContext;
        private RemoteWebDriver driver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            seleniumtests.testContext = testContext;
        }

        [TestInitialize]
        public void TestInit()
        {
            driver = GetChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
        }

        [TestCleanup]
        public void TestClean()
        {
            driver.Quit();
        }

        [TestMethod]
        public void SampleFunctionalTest1()
        {
            var webAppUrl = "";
            try
            {
                webAppUrl = testContext.Properties["webAppUrl"].ToString();
            }
            catch (Exception)
            {
                webAppUrl = "http://localhost:50433/";
            }
            //var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimestamp = startTimestamp + 60 * 10;

            string expectedValue = "Your BMI Category is Normal";


            driver.Navigate().GoToUrl("http://localhost:50433/");
            driver.Manage().Window.Size = new System.Drawing.Size(960, 1160);
            driver.FindElement(By.Id("BMI_WeightStones")).Click();
            driver.FindElement(By.Id("BMI_WeightStones")).SendKeys("12");
            driver.FindElement(By.Id("BMI_WeightPounds")).Click();
            driver.FindElement(By.Id("BMI_WeightPounds")).SendKeys("10");
            driver.FindElement(By.Id("BMI_HeightFeet")).Click();
            driver.FindElement(By.Id("BMI_HeightFeet")).SendKeys("6");
            driver.FindElement(By.Id("BMI_HeightInches")).Click();
            driver.FindElement(By.Id("BMI_HeightInches")).SendKeys("3");
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.Id("BMIValue")).Click();
            string actualValue = driver.FindElement(By.Id("BMICategory")).Text;

            Assert.AreEqual(actualValue, expectedValue);


        }

        private RemoteWebDriver GetChromeDriver()
        {
            var path = Environment.GetEnvironmentVariable("chromedriver");
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }
    }
}