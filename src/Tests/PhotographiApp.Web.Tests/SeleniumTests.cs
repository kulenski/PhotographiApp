namespace PhotographiApp.Web.Tests
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using Xunit;

    public class SeleniumTests : IClassFixture<SeleniumServerFactory<Startup>>, IDisposable
    {
        private readonly SeleniumServerFactory<Startup> server;
        private readonly IWebDriver browser;
        private bool disposed = false;

        // Be sure that selenium-server-standalone-3.141.59.jar is running
        public SeleniumTests(SeleniumServerFactory<Startup> server)
        {
            this.server = server;
            server.CreateClient();
            var opts = new ChromeOptions();
            opts.AddArguments("--headless", "--ignore-certificate-errors");
            this.browser = new RemoteWebDriver(opts);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public void FooterOfThePageContainsPrivacyLink()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);
            Assert.Contains(
                this.browser.FindElements(By.CssSelector("footer a")),
                x => x.GetAttribute("href").EndsWith("/Home/Privacy"));
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.browser.Dispose();
                    this.server.Dispose();
                }

                // Indicate that the instance has been disposed.
                this.disposed = true;
            }
        }
    }
}
