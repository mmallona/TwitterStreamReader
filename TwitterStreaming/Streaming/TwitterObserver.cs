using System;
using System.Collections.Generic;
using System.Text;
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
    public class TwitterObserver : IMetricObserver
    {
        int _numberOfTwits = 0;
        PrintScreen _writeToConsole = new PrintScreen();
        public int GetLastNumberOfTwitts()
        {
            return _numberOfTwits;
        }

        public void Update(int newTwit)
        {
            _numberOfTwits += newTwit;          

            lock(_writeToConsole )
            {
                string message = $"Number Of Twitts :{ _numberOfTwits}";
                _writeToConsole.DisplayInScreen(message, 0, 5, ConsoleColor.Yellow);
            }          

            /* Do Additional processing here if needed */
        }
    }
}
