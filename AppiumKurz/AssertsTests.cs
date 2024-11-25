using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace AppiumKurz;
public class AssertsTests
{
    private AndroidDriver _driver;
    private string _appPackage = "com.tredgate.tredgatemauitestingapp";
    private string _appActivity = "crc64f8886627d37fe620.MainActivity";
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
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }

    [Test]
    public void IsVisibleAndExist()
    {
        _driver.StartActivity(_appPackage, _appActivity);
        var element = _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/TaskEntry"));

        // Provedeme kontrolu existence elementu
        Assert.That(element, Is.Not.Null, "Prvek 'TaskEntry' neexistuje");

        // Kontrola viditelnosti prvku
        Assert.That(element.Displayed, Is.True, "Prvek 'TaskEntry' není vidět");
    }
}
