using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitterStreaming.Iterfaces;
using TwitterStreaming.Iterfactes;
using static TwitterStreaming.Iterfaces.IStreamLineReader;

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
    public class ReadStreamLines : IStreamLineReader
    {
        IStreamConnection _connection = null;
        Stream _buffer = null;
        int _numberOfTwits = 0;
        IMetricObserver _observer = null;
        PrintScreen _writeToConsole = new PrintScreen();

        public int TotalTwets { get { return _numberOfTwits; } }
        
        public ReadStreamLines(IStreamConnection connection,IMetricObserver observer)
        {
            _connection = connection;
            _buffer = _connection.Connect();
            _observer = observer;
        }

        public void ReadLines(int totalMinutesInTrial)
        {
            if (_buffer != null)
            {
                using (var reader = new StreamReader(_buffer))
                {
                    DateTime end = DateTime.Now.AddMinutes(totalMinutesInTrial);
                    while (!reader.EndOfStream && DateTime.Now <= end)
                    {
                        lock(_writeToConsole)
                        {
                            string message = $"At Minute : {end.Minute - DateTime.Now.Minute}";
                            _writeToConsole.DisplayInScreen(message, 0, 2, ConsoleColor.Magenta);
                        }    

                        var currentLine = reader.ReadLine();
                        Task.Run(() => SetCountOnLastLine(currentLine));
                    }
                }
            }
            else
                throw new Exception("The Stream buffer is not initialized");
          
        }

        protected void SetCountOnLastLine(string currentLine)
        {
            _numberOfTwits++;
            _observer.Update(_numberOfTwits);
        }
    }

    
}
