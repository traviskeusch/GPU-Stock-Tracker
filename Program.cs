using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AWSSDK;

namespace GPU_Stock_Tracker
{
    class Program
    {
        private static ChromeDriver chromeDriver { get; set; }

        private static readonly List<string> _urls = new List<string>
        {
            "https://www.newegg.com/msi-geforce-rtx-3080-rtx-3080-ventus-3x-10g/p/N82E16814137600?cm_mmc=vendor-nvidia&u1=s16145664215995o5aa52439",
            "https://www.bestbuy.com/site/nvidia-geforce-rtx-3080-10gb-gddr6x-pci-express-4-0-graphics-card-titanium-and-black/6429440.p?skuId=6429440",
            "https://www.bestbuy.com/site/evga-geforce-rtx-3080-xc3-ultra-gaming-10gb-gddr6-pci-express-4-0-graphics-card/6432400.p?skuId=6432400",
            "https://www.bestbuy.com/site/evga-geforce-rtx-3080-xc3-black-gaming-10gb-gddr6-pci-express-4-0-graphics-card/6432399.p?skuId=6432399"
        };

        


        static void Main(string[] args)
        {
            Console.WriteLine( "                    xxxxxxxx       xxxxxxxxx       x          x\r\n                 xxx              x       xx      x          x\r\n                xx                x        x      x          x\r\n                x                 x        x      x          x\r\n                x                 x       xx      x          x\r\n                x                 x xxxxxxx       x          x\r\n                x      xxxxx      x x             x          x\r\n                xx        xx      x               xx         x\r\n                 xx     xxx       x                x        xx\r\n                  xxxxxxx         x                xxxxxxxxxx\r\n\r\n\r\nxxxxxxxxxxxxx                             x\r\n      x                                    x\r\n      x                                    x                      xxxxx\r\n      x                                    x xx       xxxxx      xx\r\n      x         xxxx    xxxxx       xxx    xxx      xxx   x      x\r\n      x        xx     xxx  xx     xxx      xxxx     x   xxx      x\r\n      xx       x     xx     xx   x         x  xx    xxxxx        x\r\n      xx       x     x      xxx  xx        x   xx   xx           x\r\n      x        x     xxxxxxx  x   xxxxxx    x   xx    xxxxxx     x\r\n      x");

            for (int i = 0; i < int.MaxValue; i++)
            {
                InitializeChromedriver();

                foreach (var url in _urls)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\n Navigating to {url}");
                    chromeDriver.Navigate().GoToUrl(url);

                    if (url.Contains("newegg"))
                    {
                        var addToCartButton =
                            chromeDriver.FindElementsByXPath(
                                "//button[contains(@class, 'btn' )][contains(text(), 'Add to cart' )]");

                        foreach (var button in addToCartButton)
                        {
                            if (button.Displayed && button.Enabled)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\n AVAILABLE GPU FOUND AT {url}");
                                //SendText(url);
                                Console.ReadLine();
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n No GPU Found at {url}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n Waiting 5 seconds then moving to next URL");

                    }
                    else if (url.Contains("bestbuy"))
                    {
                        var addToCartButton =
                            chromeDriver.FindElementsByXPath(
                                "//button[contains(@class, 'btn' )][contains(text(), 'Add to Cart' )]");

                        foreach (var button in addToCartButton)
                        {
                            if (button.Displayed && button.Enabled)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\n AVAILABLE GPU FOUND AT {url}");
                                //SendText(url);
                                Console.ReadLine();
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n No GPU Found at {url}. \n Waiting 5 seconds then moving to next URL");
                    }
                }

                // Close and dispose Chromedriver instance
                QuitChromeDriver();
            }

           
        }

        public static void SendText(string url)
        {
            var message = new MailMessage();
            message.From = new MailAddress("sender@foo.bar.com");

            message.To.Add(new MailAddress("5175262955@txt.att.net"));
            message.Subject = $"RTX 3080 Available at {url}";
            message.Body = "This is the content";

            string hostName = Dns.GetHostName();
            var client = new SmtpClient(Dns.GetHostEntry(hostName).AddressList[0].ToString());
            client.Send(message);
        }



        public static void InitializeChromedriver()
        {
            var chromedriverOptions = new ChromeOptions();
            chromedriverOptions.AddArgument("--log-level=3");
            chromedriverOptions.AddArgument("--silent");
            chromeDriver = new ChromeDriver(chromedriverOptions);
        }

        public static void QuitChromeDriver()
        {
            chromeDriver.Quit();
            chromeDriver.Dispose();
        }







    }
}
