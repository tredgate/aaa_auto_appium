using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace AppiumKurz;

[TestFixture]
public class AppiumActions
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
    public void IdentifyingElementsTest()
    {
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/TaskEntry"));
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/DeadlinePicker"));
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/AddTaskButton"));
    }

    [Test]
    public void SendKeysTest()
    {
        _driver.StartActivity(_appPackage, _appActivity);
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/TaskEntry")).SendKeys("Náš první úkol");
    }

    [Test]
    public void TapTest()
    {
        _driver.StartActivity(_appPackage, _appActivity);
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/TaskEntry")).SendKeys("Test Task");
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/AddTaskButton")).Click();

    }

    [Test]
    public void WaitUntilVisibleTest()
    {
        // Přidáme úkol
        string taskText = "Test Task"; // Ukládáme, abychom následně mohli ověřit, zda se úkol zobrazil (selektor pro uložený úkol bude obsahovat tento text)
        _driver.StartActivity(_appPackage, _appActivity);
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/TaskEntry")).SendKeys(taskText);
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/AddTaskButton")).Click();
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("com.tredgate.tredgatemauitestingapp:id/task_1")));
        _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/EditTaskButton")).Click();
    }

    [Test]
    public void ScrollingTest()
    {
        _driver.StartActivity(_appPackage, _appActivity);

        int lastTask = 5;
        string taskName = "Task";
        // for to add 10 tasks and then focus on the last one
        for (int i = 0; i < lastTask; i++)
        {
            _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/TaskEntry")).SendKeys(taskName + i);
            _driver.FindElement(By.Id("com.tredgate.tredgatemauitestingapp:id/AddTaskButton")).Click();
        }
        // Proměnná pro selektor, který nám umožní scrollovat na poslední úkol. Operátory + zde slouží k zvýšení přehlednosti kódu.
        string taskScrollSelector = "new UiScrollable(new UiSelector().scrollable(true))" +
        ".scrollIntoView(new UiSelector()" +
        ".resourceId(\"com.tredgate.tredgatemauitestingapp:id/task_" + lastTask + "\"))";
        string editTaskButtonXPath = "//android.widget.FrameLayout[@resource-id=\"com.tredgate.tredgatemauitestingapp:id/task_" + lastTask + "\"]//android.widget.Button[@resource-id=\"com.tredgate.tredgatemauitestingapp:id/EditTaskButton\"]";
        _driver.FindElement(MobileBy.AndroidUIAutomator(taskScrollSelector)); // Scroll na poslední úkol
        _driver.FindElement(By.XPath(editTaskButtonXPath)).Click(); // Tapnutí na tlačítko editace posledního úkolu
    }

}

/*
 * 
 * 
 * Cvičení - Wait() (⌛10:00)
Vytvořte nový testovací soubor s testem na přihlášení a odhlášení do Tredgate Learning App.
Informace:
Název package: com.example.tredgate_learningapp
Název aktivity: .MainActivity
Název souboru: ExerciseTredgateLearningLoginTests
Název testu vytvořte sami.
Uživatelské jméno: success_user
Heslo: 123456
Kroky:
Přihlaste se
Počkejte na zobrazení nadpisu "Vítejte v Tredgate!"
Odhlaste se
*/