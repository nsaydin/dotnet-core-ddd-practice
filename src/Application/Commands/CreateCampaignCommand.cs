using Application.Dtos;
using MediatR;

namespace Application.Commands
{
    public class CreateCampaignCommand : IRequest<CampaignDto>
    {
        public string Name { get; set; }
        
        public string ProductCode { get; set; }
        
        /// <summary>
        /// this is given in hours
        /// </summary>
        public int Duration { get; set; }
        
        /// <summary>
        /// this is the maximum percentage that you can increase or decrease the price of product according to demand
        /// </summary>
        public int PriceManipulationLimit { get; set; }
        
        /// <summary>
        /// this is the product quantity you want to sell during the campaign
        /// </summary>
        public int TargetSalesCount { get; set; }
    }
}