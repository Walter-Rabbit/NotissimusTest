using NotissimusTest.Core.Models;

namespace NotissimusTest.Core.Repositories;

public interface IOffersRepository
{
    Offer GetOffer(string id);
    void AddOffer(Offer offer);
}