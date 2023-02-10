using NotissimusTest.Core.Models;

namespace NotissimusTest.Core.Services;

public interface IOffersService
{
    Offer GetOfferFromDataBase(string id);

    Task AddOfferToDataBase(string id, string ordersSourcesAddress);
}