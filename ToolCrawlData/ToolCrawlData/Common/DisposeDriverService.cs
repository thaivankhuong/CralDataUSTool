using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;

namespace ToolCrawlData.Common
{
    public static class DisposeDriverService
    {
        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000071 RID: 113 RVA: 0x000093C4 File Offset: 0x000075C4
        // (set) Token: 0x06000072 RID: 114 RVA: 0x000093CB File Offset: 0x000075CB
        public static DateTime? TestRunStartTime { get; set; }

        // Token: 0x06000073 RID: 115 RVA: 0x000093D4 File Offset: 0x000075D4
        public static void FinishHim(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Dispose();
            }
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                try
                {
                    Debug.WriteLine(process.ProcessName);
                    bool flag = process.StartTime > DisposeDriverService.TestRunStartTime;
                    if (flag)
                    {
                        bool shouldKill = false;
                        foreach (string processName in DisposeDriverService._processesToCheck)
                        {
                            bool flag2 = process.ProcessName.ToLower().Contains(processName);
                            if (flag2)
                            {
                                shouldKill = true;
                                break;
                            }
                        }
                        bool flag3 = shouldKill;
                        if (flag3)
                        {
                            process.Kill();
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        // Token: 0x0400007E RID: 126
        private static readonly List<string> _processesToCheck = new List<string>
        {
            "opera",
            "chrome",
            "firefox",
            "ie",
            "gecko",
            "phantomjs",
            "edge",
            "microsoftwebdriver",
            "webdriver"
        };
    }
}
