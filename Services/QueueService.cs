using System.Collections.Generic;
using Amazon.SQS;
using Amazon.SQS.Model;
using Services;
using Amazon;

namespace AWS.SQS.Services
{
    public class QueueService : ISQS
    {

        IAmazonSQS client;
        public QueueService()
        {
            client = new AmazonSQSClient(RegionEndpoint.USEast2); //credential picked up from credentials file in pc
        }
        public string CreateSQSQueue(string name)
        {
            try
            {
                var request = new CreateQueueRequest
                {
                    QueueName = name
                };

                var response = client.CreateQueueAsync(request);

                var retval = response.Result.QueueUrl;

                return retval;
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public List<string> ListSQSQueues()
        {
            List<string> retval = new List<string>();
            try
            {
                var request = new ListQueuesRequest();
                var response = client.ListQueuesAsync(request).Result.QueueUrls;

                retval.AddRange(response);

                return retval;
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public void SendMessage(string url, string message)
        {
            try
            {
                var request = new SendMessageRequest
                {

                    QueueUrl = url,
                    MessageBody = message


                };

                var response = client.SendMessageAsync(request).Result;

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}