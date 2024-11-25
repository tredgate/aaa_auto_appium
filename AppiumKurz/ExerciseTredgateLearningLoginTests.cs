using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace AppiumKurz;

[TestFixture]
public class ExerciseTredgateLearningLoginTests
{
    private AndroidDriver _driver;
    private string _appPackage = "com.example.tredgate_learningapp";
    private string _appActivity = ".MainActivity";
    private WebDriverWait _wait;

    [OneTimeSetUp]
    public void SetUp()
    {
        var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");
        var driverOptions = new AppiumOptions()
        {
            AutomationName = AutomationName.AndroidUIAutomator2,
            PlatformName = "Android",
        };

        driverOptions.AddAdditionalAppiumOption("appPackage", _appPackage);
        driverOptions.AddAdditionalAppiumOption("appActivity", _appActivity);
        driverOptions.AddAdditionalAppiumOption("noReset", true);
        _driver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180));
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }

    [Test]
    public void TredgateLoginTest()
    {
        _driver.StartActivity(_appPackage, _appActivity);
        _driver.FindElement(By.Id("com.example.tredgate_learningapp:id/username_fieldcom.example.tredgate_learningapp:id/username_field")).SendKeys("success_user");
        _driver.FindElement(By.Id("com.example.tredgate_learningapp:id/pin_field")).SendKeys("123456");
        _driver.FindElement(By.Id("com.example.tredgate_learningapp:id/submit_button")).Click();

    }
}

/*
 dotnet add package DotNetSeleniumExtras.WaitHelpers

 */