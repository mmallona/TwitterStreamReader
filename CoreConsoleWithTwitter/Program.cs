using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterStreaming;
using TwitterStreaming.Iterfaces;
using TwitterStreaming.Iterfactes;
using TwitterStreaming.Streaming;

/*
 *  Author : Martin Mallona
 *  Simple multi tasked Program to read the part of the 1% feed from Twitter.
 *  As the feed is being read it displays max number of minutes left
 *  and the current count.
 *  
 *  When is is done, it show the total lines read and the average per minute
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
 *  THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
 *  OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 *  ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */

/// <summary>
///  You will need to register at Twitter and get a token which should
///  be included in the App.config file for both the program and the tests
/// </summary>

namespace CoreConsoleWithTwitter
{

    class Program
    {
        static void Main(string[] args)//
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            string url = System.Configuration.ConfigurationManager.AppSettings.Get("url"); ;
            string token = System.Configuration.ConfigurationManager.AppSettings.Get("token");
            const int MAX_TRIAL_MINUTES = 3;
            const string BEARER = "bearer ";
            try
            {
                PrintScreen _writeToConsole = new PrintScreen();
                string initMsg = $"Starting Total and Avg at : {MAX_TRIAL_MINUTES} Minutes, Please wait";
                _writeToConsole.DisplayInScreen(initMsg, 0, 0);

                ///  This is the Call to start collecting the Twits
                ITwitterConsumer consumer = new TwitterConsumer(MAX_TRIAL_MINUTES, url, BEARER + token);
                consumer.Start();                


                //// Get the Total number of Twits
                string totalTwits = $"Total Twists : {consumer.GetNumberOfTwits()}";
                _writeToConsole.DisplayInScreen(totalTwits, 0, 10, ConsoleColor.Green);

                //// Get the Average per minute
                string totalTwitsPerMinute = $"Total Twists Per Minute : {consumer.GetNumberOfTwitsPerMinute()}";
                _writeToConsole.DisplayInScreen(totalTwitsPerMinute, 0, 11, ConsoleColor.Cyan);

                string endingMsg = "/////////////////////// Ending Program ///////////////////////////////////";
                _writeToConsole.DisplayInScreen(endingMsg, 0, 12, ConsoleColor.Cyan);

            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

    }
}
