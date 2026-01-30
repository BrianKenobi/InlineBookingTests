using Microsoft.Playwright;

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 500 });
var context = await browser.NewContextAsync(new BrowserNewContextOptions { ViewportSize = new ViewportSize { Width = 1920, Height = 1080 } });
var page = await context.NewPageAsync();

await page.GotoAsync("https://inline.app/booking/-Lamo24uNMzLIlnCEhIJ:inline-live-2a466/-Lamo28zt1ere32YxWMR?language=en", new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

await page.Locator("#adult-picker").SelectOptionAsync("3");
await Task.Delay(500);

await page.EvaluateAsync(@"() => {
    const btn = document.querySelector('[data-cy=""target-date""]');
    if (btn) btn.click();
}");

var dateSelector = "[data-cy='bt-cal-day'][data-date='2026-02-11']";
await page.WaitForSelectorAsync(dateSelector, new() { State = WaitForSelectorState.Attached });
await page.Locator(dateSelector).DispatchEventAsync("click");

var timeBoxSelector = "[data-cy='book-now-time-slot-box-12-00']";
await page.WaitForSelectorAsync(timeBoxSelector);
await page.Locator(timeBoxSelector).DispatchEventAsync("click");

await page.GetByRole(AriaRole.Button, new() { Name = "Complete booking" }).ClickAsync();

await page.ScreenshotAsync(new PageScreenshotOptions { Path = "booking_result.png", FullPage = true });
Console.WriteLine("�y�{���槹��");

await browser.CloseAsync();