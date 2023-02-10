using NotissimusTest.Core.Exceptions;
using NotissimusTest.Core.Models;
using NotissimusTest.Core.Repositories;

namespace NotissimusTest.Data.Repositories;

public class OffersRepository : IOffersRepository
{
    private NotissimusTestContext _context;
    
    public OffersRepository(NotissimusTestContext context)
    {
        _context = context;
    }
    
    public Offer GetOffer(string id)
    {
        var offer = _context.Offers.FirstOrDefault(o => o.Id == id);
        if (offer is null)
        {
            throw new NotFoundException($"There is no order with such id {id}.");
        }

        return offer;
    }

    public void AddOffer(Offer offer)
    {
        if (_context.Offers.All(o => o.Id != offer.Id))
        {
            _context.Offers.Add(offer);
        }

        _context.SaveChanges();
    }
}