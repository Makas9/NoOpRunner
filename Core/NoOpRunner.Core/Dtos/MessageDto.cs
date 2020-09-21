using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Dtos
{
    public class MessageDto
    {
        public MessageType MessageType { get; set; }

        public object Payload { get; set; }
    }
}
