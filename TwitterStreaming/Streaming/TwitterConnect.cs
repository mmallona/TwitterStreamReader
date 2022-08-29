using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterStreaming.Iterfaces;

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

namespace TwitterStreaming
{
    public class TwitterConnect : IStreamConnection
    {
        string _url = string.Empty;
        string _token = string.Empty;
        static readonly HttpClient client = new HttpClient();

        public TwitterConnect(string url,string token)
        {
            _url = url;
            _token = token;
        }
        public Stream Connect()
        {            
            Stream stream = null;
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", _token);
                stream =  client.GetStreamAsync(_url).Result;
            }
            catch(AggregateException ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }

            return stream;
        }
    }
}
