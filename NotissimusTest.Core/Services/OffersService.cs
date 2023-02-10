using System.Text;
using System.Xml;
using NotissimusTest.Core.Exceptions;
using NotissimusTest.Core.Models;
using NotissimusTest.Core.Repositories;
using XmlException = NotissimusTest.Core.Exceptions.XmlException;

namespace NotissimusTest.Core.Services;

public class OffersService : IOffersService
{
    private readonly IOffersRepository _offersRepository;
    private readonly HttpClient _httpClient;
    private readonly XmlDocument _xmlDocument;

    public OffersService(IOffersRepository offersRepository, HttpClient httpClient, XmlDocument xmlDocument)
    {
        _offersRepository = offersRepository;
        _httpClient = httpClient;
        _xmlDocument = xmlDocument;
    }

    public Offer GetOfferFromDataBase(string id)
    {
        return _offersRepository.GetOffer(id);
    }

    public async Task AddOfferToDataBase(string id, string ordersSourcesAddress)
    {
        var xmlOrders = await GetXmlOrders(ordersSourcesAddress);
        var xmlOrder = xmlOrders.ChildNodes.OfType<XmlElement>().FirstOrDefault(order =>
            order.Attributes.OfType<XmlAttribute>().FirstOrDefault(at => at.Name == "id")?.Value == id
        );

        if (xmlOrder is null)
        {
            throw new NotFoundException($"There is no order with such id: {id}");
        }

        var offer = new Offer(id, xmlOrder.OuterXml);
        _offersRepository.AddOffer(offer);
    }

    private async Task<XmlNode> GetXmlOrders(string ordersSourceAddress)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            ordersSourceAddress);
        var response = await _httpClient.SendAsync(request);
        var streamContent = await response.Content.ReadAsStreamAsync();
        var content = await new StreamReader(streamContent, Encoding.GetEncoding("windows-1251")).ReadToEndAsync();

        if (content is null)
        {
            throw new ConfigurationException("Offers source address is incorrect.");
        }

        _xmlDocument.LoadXml(content);
        var shopNode = _xmlDocument.DocumentElement?.OfType<XmlNode>().First() ??
                       throw new XmlException("Incorrect xml document.");

        return shopNode.ChildNodes.OfType<XmlNode>().FirstOrDefault(child => child.Name == "offers") ??
               throw new XmlException("There is no offers in document");
    }
}