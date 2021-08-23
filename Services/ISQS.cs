using System.Collections.Generic;

namespace Services
{
    public interface ISQS
    {
        string CreateSQSQueue(string name);
        List<string> ListSQSQueues();

        void SendMessage(string url, string message);
    }
}