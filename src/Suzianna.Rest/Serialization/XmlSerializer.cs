using YAXLib;

namespace Suzianna.Rest.Serialization
{
    internal class XmlSerializer : ISerializer
    {
        public string Serialize(object objectToSerialize)
        {
            var serializer = new YAXSerializer(objectToSerialize.GetType());
            return serializer.Serialize(objectToSerialize);
        }
    }
}