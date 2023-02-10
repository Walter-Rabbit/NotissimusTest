using System.Xml;
using System.Xml.Linq;

namespace NotissimusTest.Core.Models;

public class Offer
{
    public Offer(string id, string serializedOffer)
    {
        Id = id;
        SerializedOffer = serializedOffer;
    }

    public string Id { get; set; }
    public string SerializedOffer { get; set; }

    public XElement GetXmlOffer()
    {
        return XElement.Parse(SerializedOffer);
    }
}