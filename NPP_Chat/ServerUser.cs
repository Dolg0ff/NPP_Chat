using System.ServiceModel;

namespace NPP_Chat
{
    class ServerUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
