using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumKurz;

[TestFixture]
public class FirstAppiumTest
{
    private AndroidDriver _driver;

    [OneTimeSetUp]
    public void SetUp()
    {
        var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/"); // Nastavujeme url Appium serveru pro ovládání aplikace
        var driverOptions = new AppiumOptions() // Vytváříme AppiumOptions, které budou ovládat AndroidDriver
        {
            AutomationName = AutomationName.AndroidUIAutomator2, // Nastavujeme ovladač pro Appium, AndroidUIAutomator2
            PlatformName = "Android",
        };

        driverOptions.AddAdditionalAppiumOption("appPackage", "com.example.tredgate_learningapp"); // Identikace balíčku aplikace
        driverOptions.AddAdditionalAppiumOption("appActivity", ".MainActivity"); // Identikace hlavní aktivity aplikace
        // NoReset assumes the app com.google.android is preinstalled on the emulator
        driverOptions.AddAdditionalAppiumOption("noReset", true);

        _driver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180)); // Nastavujeme AndroidDriver, který bude ovládat aplikaci s  parametry: serverUri, driverOptions, timeout

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Nastavujeme implicitní čekání na 10 sekund na nalezení elementu
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }

    [Test]
    public void AppiumTest()
    {
        _driver.StartActivity("com.example.tredgate_learningapp", ".MainActivity"); // Spouštíme aktivitu aplikace Tredgate Learning App
        _driver.FindElement(By.Id("com.example.tredgate_learningapp:id/username_field")).SendKeys("První test"); // Najdeme prvek s ID username_field a pošleme do něj text "První test"

    }

    [Test]
    public void LoginErrorMessageTest()
    {
        _driver.StartActivity("com.example.tredgate_learningapp", ".MainActivity");
        _driver.FindElement(By.Id("com.example.tredgate_learningapp:id/submit_button")).Click();
    }
}
/*
 * Vytvoření Appium testu (⌛6:31)
Vytvořte nový test (metodu) uvnitř souboru: FirstAppiumTests.cs v jeho třídě.
Název metody: LoginErrorMessageTest
Kroky:
 _driver.StartActivity("com.example.tredgate_learningapp", ".MainActivity");
_driver.FindElement(By.Id("com.example.tredgate_learningapp:id/submit_button")).Click(); 
Spusť test pomocí příkazu: dotnet test --filter LoginErrorMessageTest
 a zkontroluj, že se ti v zařízení zobrazí zpráva o špatně zadaných údajích:
 */