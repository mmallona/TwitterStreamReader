using Moq;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
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

namespace TestTwitterStreams
{
    public class TestTwitterStreams
    {

        const string BEARER = "bearer ";
        [SetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", String.Format("{0}\\app.config", AppDomain.CurrentDomain.BaseDirectory));
        }

        [Test]
        public void TestConnection()
        {
            // Arrange

            string url = ConfigurationManager.AppSettings.Get("url"); ;
            string token = ConfigurationManager.AppSettings.Get("token");

            // Act
            IStreamConnection connect = new TwitterConnect(url, BEARER + token);

            // Assert
            Assert.IsNotNull(connect.Connect() is Stream);
        }
        [Test]
        public void ObserverNumberOfTwits()
        {
            // Arrange
            IMetricObserver observer = new TwitterObserver();

            // Act
            int result = observer.GetLastNumberOfTwitts();
            int expected = 0;

            // Assert
            Assert.IsTrue(result == expected);
        }
    }
}