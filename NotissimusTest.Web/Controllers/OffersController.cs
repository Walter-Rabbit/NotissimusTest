using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using NotissimusTest.Core.Exceptions;
using NotissimusTest.Core.Models;
using NotissimusTest.Core.Services;

namespace NotissimusTest.Web.Controllers;

public class OffersController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IOffersService _offersService;

    public OffersController(IConfiguration configuration, IOffersService offersService)
    {
        _configuration = configuration;
        _offersService = offersService;
    }

    public Offer GetOfferFromDataBase()
    {
        var id = _configuration["OfferId"] ??
                 throw new ConfigurationException("There is no offers id in configuration.");
        return _offersService.GetOfferFromDataBase(id);
    }

    public async Task AddOfferToDataBase()
    {
        var id = _configuration["OfferId"] ??
                 throw new ConfigurationException("There is no offers id in configuration.");

        var offersSourceAddress = _configuration["OffersSource"] ??
                                  throw new ConfigurationException("There is no offers source in configuration.");

        await _offersService.AddOfferToDataBase(id, offersSourceAddress);
    }

    public async Task<IActionResult> Index()
    {
        await AddOfferToDataBase();
        var offer = GetOfferFromDataBase();
        var xmlOffer = offer.GetXmlOffer();
        foreach (var node in xmlOffer.Descendants())
        {
            ViewData[node.Name.LocalName] = node.Value;
        }

        ViewData["id"] = offer.Id;
        return View();
    }
}