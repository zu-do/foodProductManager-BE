using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace PVP_Projektas_API.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly ProjectDbContext _dbContext;

        public ReservationRepository(ProjectDbContext db)
        {
            _dbContext = db;
        }

        public async Task<Reservation> Create(int productId, int GivesUserId, int TakesUserId)
        {
            var newReservation = new Reservation
            {
                Date = DateTime.Now,
                Done = false,
                GivesUserId = GivesUserId,
                TakesUserId = TakesUserId,
                ProductId = productId,
            };
            _dbContext.DbReservations.Add(newReservation);
            await _dbContext.SaveChangesAsync();
            return newReservation;
        }
    }
}
