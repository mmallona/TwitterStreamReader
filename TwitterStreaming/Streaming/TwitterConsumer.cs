using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterStreaming.Iterfaces;
using TwitterStreaming.Iterfactes;

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

namespace TwitterStreaming.Streaming
{
    public class TwitterConsumer : ITwitterConsumer
    {
        protected int _totalTwits = 0;
        protected int _maxNumberOfMinutes = 0;
        protected string _url = string.Empty;
        protected string _token = string.Empty;

        IMetricObserver observer = null;

        public TwitterConsumer(int maxNumberOfMinutes,string url,string token)
        {
            _maxNumberOfMinutes = maxNumberOfMinutes;
            _url = url;
            _token = token;
        }
        public void  ExecuteReader()
        {
            IStreamConnection connect = new TwitterConnect(_url, _token);
            observer = new TwitterObserver();

            IStreamLineReader streamLine = new ReadStreamLines(connect,observer);

            try
            {
                streamLine.ReadLines(_maxNumberOfMinutes);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("The Program is ending without processing any Twits");
            }
            
        }

        /// <summary>
        /// Start it and wait until done
        /// </summary>
        public void Start()
        {
            var processingTask = Task.Run(() => ExecuteReader());
            processingTask.Wait();
        }       

        public int GetNumberOfTwits()
        {
            return observer.GetLastNumberOfTwitts();
        }

        public decimal GetNumberOfTwitsPerMinute()
        {
            return observer.GetLastNumberOfTwitts() / _maxNumberOfMinutes;
        }   
    }
}
